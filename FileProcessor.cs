using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CSFormat
{
    public class FileProcessor
    {
        private const int BufferSize = 4096; // Tamaño del búfer para lectura de archivos grandes
        private readonly IProgress<int>? _progress;

        public FileProcessor(IProgress<int>? progress = null)
        {
            _progress = progress;
        }

        public string ProcessFile(string formatFilePath, string inputFilePath, string separator, bool hasTitles)
        {
            // Leer y validar el archivo de formato
            var fieldDefinitions = ReadFormatFile(formatFilePath);
            
            // Procesar el archivo de entrada y escribir el de salida
            string outputFilePath = GetOutputFilePath(inputFilePath);
            
            // Usar StreamWriter con StringBuilder para escritura eficiente
            using (var writer = new StreamWriter(outputFilePath, false, Encoding.UTF8, BufferSize))
            {
                // Escribir encabezados si es necesario
                if (hasTitles)
                {
                    var headerLine = string.Join(separator, fieldDefinitions.Select(f => f.name));
                    writer.WriteLine(headerLine);
                }

                // Contar el total de líneas para el progreso
                int totalLines = File.ReadLines(inputFilePath).Count(line => !string.IsNullOrWhiteSpace(line.Trim()));
                if (totalLines == 0)
                {
                    throw new Exception("El archivo de entrada está vacío o solo contiene líneas en blanco.");
                }

                // Procesar y escribir líneas del archivo de entrada
                int lineCount = 0;
                int lastReportedPercentage = -1;

                foreach (var line in ReadInputLines(inputFilePath))
                {
                    var formattedLine = FormatLine(line, fieldDefinitions, separator);
                    if (!string.IsNullOrEmpty(formattedLine))
                    {
                        writer.WriteLine(formattedLine);
                        lineCount++;
                        
                        // Reportar progreso (solo si cambió el porcentaje)
                        int percentage = (int)((double)lineCount / totalLines * 100);
                        if (percentage != lastReportedPercentage)
                        {
                            _progress?.Report(percentage);
                            lastReportedPercentage = percentage;
                            
                            // Mostrar progreso en consola cada 5% o cada 1000 líneas
                            if (percentage % 5 == 0 || lineCount % 1000 == 0)
                            {
                                Console.WriteLine($"Procesadas {lineCount} de {totalLines} líneas ({percentage}%)");
                            }
                        }
                    }
                }
                
                Console.WriteLine($"Total de líneas procesadas: {lineCount}");
            }

            // Verificar el archivo de salida
            VerifyOutputFile(outputFilePath);
            
            return outputFilePath;
        }

        private List<(string name, int length)> ReadFormatFile(string formatFilePath)
        {
            var fieldDefinitions = new List<(string name, int length)>();
            
            foreach (var line in File.ReadLines(formatFilePath))
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                
                var trimmedLine = line.Trim();
                var parts = trimmedLine.Split(',');
                
                if (parts.Length >= 2 && int.TryParse(parts[1].Trim(), out int length))
                {
                    fieldDefinitions.Add((parts[0].Trim(), length));
                }
                else
                {
                    Console.WriteLine($"ADVERTENCIA: Formato de línea no válido: '{trimmedLine}'");
                }
            }

            if (fieldDefinitions.Count == 0)
            {
                throw new Exception("No se encontraron definiciones de campo válidas en el archivo de formato.");
            }

            Console.WriteLine($"Se encontraron {fieldDefinitions.Count} definiciones de campo:");
            foreach (var (name, length) in fieldDefinitions)
            {
                Console.WriteLine($"- {name}: {length} caracteres");
            }

            return fieldDefinitions;
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
            var outputFields = new List<string>();
            int currentPosition = 0;

            foreach (var (name, length) in fieldDefinitions)
            {
                if (currentPosition >= line.Length)
                {
                    outputFields.Add(string.Empty);
                    continue;
                }


                int actualLength = Math.Min(length, line.Length - currentPosition);
                string fieldValue = line.Substring(currentPosition, actualLength).Trim();
                outputFields.Add(fieldValue);
                currentPosition += length;
            }

            return string.Join(separator, outputFields);
        }

        private string GetOutputFilePath(string inputFilePath)
        {
            return Path.Combine(
                Path.GetDirectoryName(inputFilePath) ?? string.Empty,
                $"{Path.GetFileNameWithoutExtension(inputFilePath)}_formatted{Path.GetExtension(inputFilePath)}");
        }

        private void VerifyOutputFile(string outputFilePath)
        {
            if (File.Exists(outputFilePath))
            {
                var lineCount = 0;
                using (var reader = new StreamReader(outputFilePath))
                {
                    while (reader.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }
                Console.WriteLine($"Archivo de salida verificado. Contiene {lineCount} líneas.");
            }
            else
            {
                Console.WriteLine("ERROR: No se pudo crear el archivo de salida");
            }
        }
    }
}
