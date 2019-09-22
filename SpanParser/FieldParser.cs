using System;

namespace SpanParser
{
    public class FieldParser
    {
        public string Name { get; }
        public int Index { get; }
        public int Length { get; }
        public FieldType Type { get; }

        public FieldParser(string name, int index, int length, FieldType type)
        {
            Name   = name;
            Index  = index;
            Length = length;
            Type   = type;
        }

        public object Value(ReadOnlySpan<char> lineData)
        {
            return null;
        }
    }
}
