using System;

namespace SpanParser
{
    public static class SpanToFloat
    {
        public static float UseLoop(ReadOnlySpan<char> slice, float incrementer = .01f)
        {
            if (slice == null || slice.IsEmpty) {
                return 0.00f;
            }
            float result = 0.00f;
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
    }
}
