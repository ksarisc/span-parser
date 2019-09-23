using System;

namespace SpanParser
{
    public static class SpanToInt
    {
        public static int UseLoop(ReadOnlySpan<char> slice)
        {
            if (slice == null || slice.IsEmpty) {
                return 0;
            }
            int result = 0, incrementer = 1;
            for (int i = slice.Length - 1; i >= 0; i--) {
                if (Char.IsWhiteSpace(slice[i])) {
                    // otherwise skip this index?
                    return result;
                }
                result += (Enums.CharToInt(slice[i])
                            * incrementer);
                incrementer *= 10;
            }
            return result;
        } // END UseLoop

        // public static int SpanToInt(this ReadOnlySpan<char> slice)
        // {
        //     return UseLoop(slice);
        // }

        public static int UseString(ReadOnlySpan<char> slice)
        {
            if (slice == null || slice.IsEmpty) {
                return 0;
            }
            return int.Parse(new string(slice.ToArray()));
        } // END UseString
    }
}
