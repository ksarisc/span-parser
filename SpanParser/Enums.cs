using System;

namespace SpanParser
{
    public enum FieldType
    {
        String, Int, Float, Date, Time, TStamp
    }

    internal static class Enums
    {
        public static int CharToInt(in char value)
        {
            //Char.IsWhiteSpace(value)
            if (value == '0' || value == ' ') {
                return 0;
            }
            return value - '0';
            // if (value <= 57  && value > 48) {
            // if (value <= '9' && value > '0') {
            //     return value - '0';
            // }
            //throw new ArgumentOutOfRangeException("value MUST be a number");
            // return 0;
        } // END CharToInt
    }
}
