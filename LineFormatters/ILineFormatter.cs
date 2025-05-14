using System.Collections.Generic;

namespace CSFormat.LineFormatters
{
    public interface ILineFormatter
    {
        string FormatLine(string line, List<(string name, int length)> fieldDefinitions, string separator);
    }
}
