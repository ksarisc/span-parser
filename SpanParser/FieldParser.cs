using System;

namespace SpanParser
{
    public class FieldParser
    {
        public string Name { get; }
        public int Index { get; }
        public int Length { get; }
        public FieldType Type { get; }

        private readonly bool trim;

        public FieldParser(string name, int index, int length,
                        FieldType type, bool trimData = true)
        {
            if (index < 0) {
                throw new ArgumentOutOfRangeException("Index MUST be 0 or greater");
            }
            Name   = name;
            Index  = index;
            Length = length;
            Type   = type;
            trim   = trimData;
        }

        public object Value(ReadOnlySpan<char> lineData)
        {
            int length = Length;
            if (length == -1) {
                length = lineData.Length - Index;
            }
            // check for empty/null?
            if (Type == FieldType.Float) {
                return SpanToFloat.UseLoop(lineData.Slice(Index, length));
            }
            if (Type == FieldType.Int) {
                return SpanToInt.UseLoop(lineData.Slice(Index, length));
            }
            // FieldType.Date
            // FieldType.Time
            // FieldType.TStamp
            if (Type == FieldType.String) {
                int index = Index;
                if (trim) {
                    // loop forward until first non-whitespace
                    // character found for start index

                    // then do same search with backwards loop
                }
                return new string(
                        lineData.Slice(index, length).ToArray());
            }
            return null;
        }
    }
}
