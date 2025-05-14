using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CSFormat.LineFormatters;

namespace CSFormat
{
    public class FileProcessor
    {
        private const int BufferSize = 4096;
        private readonly IProgress<int>? _progress;
        private readonly ILineFormatter _lineFormatter;

        public FileProcessor(IProgress<int>? progress = null, ILineFormatter? lineFormatter = null)
        {
            _progress = progress ?? throw new ArgumentNullException(nameof(progress));
            _lineFormatter = lineFormatter ?? new FixedWidthLineFormatter();
        }

        public string ProcessFile(string formatFilePath, string inputFilePath, string separator, bool hasTitles)
        {
            if (string.IsNullOrEmpty(formatFilePath)) throw new ArgumentNullException(nameof(formatFilePath));
            if (string.IsNullOrEmpty(inputFilePath)) throw new ArgumentNullException(nameof(inputFilePath));
            if (string.IsNullOrEmpty(separator)) throw new ArgumentNullException(nameof(separator));

            var fieldDefinitions = ReadFormatFile(formatFilePath);
            string outputFilePath = GetOutputFilePath(inputFilePath);
            
            ProcessInputFile(inputFilePath, outputFilePath, fieldDefinitions, separator, hasTitles);
            VerifyOutputFile(outputFilePath);
            
            return outputFilePath;
        }

        private void ProcessInputFile(string inputFilePath, string outputFilePath, 
            List<(string name, int length)> fieldDefinitions, string separator, bool hasTitles)
        {
            using var writer = new StreamWriter(outputFilePath, false, Encoding.UTF8, BufferSize);
            
            WriteHeaderIfNeeded(writer, fieldDefinitions, separator, hasTitles);
            
            int totalLines = CountNonEmptyLines(inputFilePath);
            if (totalLines == 0)
            {
                throw new Exception("El archivo de entrada está vacío o solo contiene líneas en blanco.");
            }

            ProcessFileLines(inputFilePath, writer, fieldDefinitions, separator, totalLines);
        }

        private void WriteHeaderIfNeeded(StreamWriter writer, 
            List<(string name, int length)> fieldDefinitions, string separator, bool hasTitles)
        {
            if (hasTitles)
            {
                var headerLine = string.Join(separator, fieldDefinitions.Select(f => f.name));
                writer.WriteLine(headerLine);
            }
        }

        private int CountNonEmptyLines(string filePath)
        {
            return File.ReadLines(filePath).Count(line => !string.IsNullOrWhiteSpace(line.Trim()));
        }

        private void ProcessFileLines(string inputFilePath, StreamWriter writer, 
            List<(string name, int length)> fieldDefinitions, string separator, int totalLines)
        {
            int lineCount = 0;
            int lastReportedPercentage = -1;

            foreach (var line in ReadInputLines(inputFilePath))
            {
                ProcessSingleLine(line, writer, fieldDefinitions, separator, ref lineCount, totalLines, ref lastReportedPercentage);
            }
            
            Console.WriteLine($"Total de líneas procesadas: {lineCount}");
        }

        private void ProcessSingleLine(string line, TextWriter writer, 
            List<(string name, int length)> fieldDefinitions, string separator,
            ref int lineCount, int totalLines, ref int lastReportedPercentage)
        {
            var formattedLine = FormatLine(line, fieldDefinitions, separator);
            if (string.IsNullOrEmpty(formattedLine)) return;
            
            writer.WriteLine(formattedLine);
            lineCount++;
            UpdateProgress(lineCount, totalLines, ref lastReportedPercentage);
        }

        private void UpdateProgress(int lineCount, int totalLines, ref int lastReportedPercentage)
        {
            int percentage = (int)((double)lineCount / totalLines * 100);
            if (percentage != lastReportedPercentage)
            {
                _progress?.Report(percentage);
                lastReportedPercentage = percentage;
                
                if (percentage % 5 == 0 || lineCount % 1000 == 0)
                {
                    Console.WriteLine($"Procesadas {lineCount} de {totalLines} líneas ({percentage}%)");
                }
            }
        }

        private List<(string name, int length)> ReadFormatFile(string formatFilePath)
        {
            var fieldDefinitions = File.ReadLines(formatFilePath)
                .Select(line => line.Trim())
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(ParseFormatLine)
                .Where(field => field != null)
                .Select(field => field!.Value)
                .ToList();

            if (fieldDefinitions.Count == 0)
            {
                throw new Exception("No se encontraron definiciones de campo válidas en el archivo de formato.");
            }

            LogFieldDefinitions(fieldDefinitions);
            return fieldDefinitions;
        }

        private static (string name, int length)? ParseFormatLine(string line)
        {
            var parts = line.Split(',');
            if (parts.Length >= 2 && int.TryParse(parts[1].Trim(), out int length))
            {
                return (parts[0].Trim(), length);
            }
            
            Console.WriteLine($"ADVERTENCIA: Formato de línea no válido: '{line}'");
            return null;
        }

        private void LogFieldDefinitions(List<(string name, int length)> fieldDefinitions)
        {
            Console.WriteLine($"Se encontraron {fieldDefinitions.Count} definiciones de campo:");
            foreach (var (name, length) in fieldDefinitions)
            {
                Console.WriteLine($"- {name}: {length} caracteres");
            }
        }

        private IEnumerable<string> ReadInputLines(string inputFilePath)
        {
            using var reader = new StreamReader(inputFilePath, Encoding.UTF8, true, BufferSize);
            string? line;
            bool firstLine = true;
            
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                var trimmedLine = line.Trim();
                if (firstLine)
                {
                    Console.WriteLine($"Primera línea de entrada: '{trimmedLine}'");
                    firstLine = false;
                }
                
                yield return trimmedLine;
            }
        }

        private string FormatLine(string line, List<(string name, int length)> fieldDefinitions, string separator)
        {
            return _lineFormatter.FormatLine(line, fieldDefinitions, separator);
        }

        private string GetOutputFilePath(string inputFilePath)
        {
            string directory = Path.GetDirectoryName(inputFilePath) ?? string.Empty;
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputFilePath);
            string extension = Path.GetExtension(inputFilePath);
            
            return Path.Combine(directory, $"{fileNameWithoutExt}_formatted{extension}");
        }

        private void VerifyOutputFile(string outputFilePath)
        {
            if (File.Exists(outputFilePath))
            {
                int lineCount = CountLinesInFile(outputFilePath);
                Console.WriteLine($"Archivo de salida verificado. Contiene {lineCount} líneas.");
            }
            else
            {
                Console.WriteLine("ERROR: No se pudo crear el archivo de salida");
            }
        }

        private int CountLinesInFile(string filePath)
        {
            using var reader = new StreamReader(filePath);
            int lineCount = 0;
            while (reader.ReadLine() != null)
            {
                lineCount++;
            }
            return lineCount;
        }
    }
}
