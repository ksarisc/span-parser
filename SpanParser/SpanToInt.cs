using System;

namespace SpanParser
{
    public static class SpanToInt
    {
        private static int ToInt(in char value)
        {
            //Char.IsWhiteSpace(value)
            if (value == '0' || value == ' ') {
                return 0;
            }
            return value - '0';
            // if (value > '0' && value <= '9') {
            //     return value - '0';
            // }
            //throw new ArgumentOutOfRangeException("value MUST be a number");
            // return 0;
        } // END ToInt

        public static int UseLoop(ReadOnlySpan<char> slice)
        {
            if (slice == null || slice.IsEmpty) {
                return 0;
            }
            int result = 0, incrementer = 1;
            for (int i = slice.Length - 1; i > 0; i--) {
                if (Char.IsWhiteSpace(slice[i])) {
                    // otherwise skip this index?
                    return result;
                }
                result += (ToInt(slice[i]) * incrementer);
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
