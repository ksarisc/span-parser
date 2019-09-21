using System;
using System.Collections.Generic;
using System.Diagnostics;
using SpanParser;

namespace Tests
{
    class Program
    {
        private delegate int ParseSpan(ReadOnlySpan<char> value);
        static void Main(string[] args)
        {
            int length  = 500000;
            var toparse = BuildArray(length).AsSpan();
            
            var swatch  = new Stopwatch();
            var parse1  = new List<int>();
            var parse2  = new List<int>();
            WriteInfo();

            RunTest(toparse, length, swatch, parse1, SpanToInt.UseLoop);
            WriteInfo(swatch.ElapsedTicks, parse1.Count);
            
            RunTest(toparse, length, swatch, parse2, SpanToInt.UseString);
            WriteInfo(swatch.ElapsedTicks, parse2.Count);

            // compare the results
        } // END Main

        private static void WriteInfo(long elapsed = -1, int count = -1)
        {
        }

        private static void RunTest(ReadOnlySpan<char> span,
                                int spanLen, Stopwatch stopWatch,
                                List<int> parsed, ParseSpan parse)
        {
            int start = 0, size = 6;
            stopWatch.Restart();
            while (start + size < spanLen) {
                parsed.Add(
                    parse(span.Slice(start, size))
                );
                NextSlice(ref start, ref size);
            }
            parsed.Add(
                SpanToInt.UseLoop(span.Slice(start, spanLen - start))
            );
            stopWatch.Stop();
        } // END RunTest

        private static readonly char[] numbers = new char[] {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };
        private static char[] BuildArray(int length)
        {
            int max  = numbers.Length - 1;
            var rand = new Random();
            var temp = new char[length];
            for (int i = 0; i < length; i++) {
                temp[i] = numbers[rand.Next(0, max)];
            }
            return temp; //.ToArray();
        } // END BuildArray

        private static void NextSlice(ref int index, ref int length)
        {
            index += length;
            switch (length) {
                case 6:
                    length = 8;
                    break;
                case 8:
                    length = 3;
                    break;
                case 3:
                    length = 5;
                    break;
                default:
                    length = 6;
                    break;
            }
        } // END NextSlice
    }
}
