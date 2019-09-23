using System;

namespace SpanParser
{
    public interface ILineParser
    {
        int Match { get; }
        object[] Parse(ReadOnlySpan<char> line);
    }

    // public class LineParser : ILineParser
    // {
    //     public LineParser(string match, LineField[] fields)
    //     {
    //     }
    // }
}
