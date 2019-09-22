using System;

namespace SpanParser
{
    public interface ILineParser
    {
        object[] Parse(ReadOnlySpan<char> line);
    }
}
