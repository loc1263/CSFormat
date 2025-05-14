using System;
using System.Collections.Generic;
using System.Linq;

namespace CSFormat.LineFormatters
{
    public class FixedWidthLineFormatter : ILineFormatter
    {
        public string FormatLine(string line, List<(string name, int length)> fieldDefinitions, string separator)
        {
            if (string.IsNullOrEmpty(line)) return string.Empty;
            if (fieldDefinitions == null) throw new ArgumentNullException(nameof(fieldDefinitions));
            if (separator == null) throw new ArgumentNullException(nameof(separator));

            var outputFields = new List<string>();
            int currentPosition = 0;

            foreach (var (_, length) in fieldDefinitions)
            {
                string fieldValue = currentPosition >= line.Length 
                    ? string.Empty 
                    : GetFieldValue(line, currentPosition, length);
                
                outputFields.Add(fieldValue);
                currentPosition += length;
            }

            return string.Join(separator, outputFields);
        }

        private static string GetFieldValue(string line, int start, int length)
        {
            int remainingLength = line.Length - start;
            int actualLength = Math.Min(length, remainingLength);
            return line.Substring(start, actualLength).Trim();
        }
    }
}
