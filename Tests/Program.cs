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
            //var slices  = BuildSlices(toparse);
            
            var swatch  = new Stopwatch();
            var parse1  = new List<int>();
            var parse2  = new List<int>();
            WriteInfo();

            RunTest(toparse, length, swatch, parse1, SpanToInt.UseLoop);
            WriteInfo(swatch.ElapsedTicks, parse1.Count);
            
            RunTest(toparse, length, swatch, parse2, SpanToInt.UseString);
            WriteInfo(swatch.ElapsedTicks, parse2.Count);

            // compare the results
            int count1 = parse1.Count;
            if (count1 != parse2.Count) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine("Counts DON'T MATCH: {0} x {1}",
                            count1, parse2.Count);
                Console.ForegroundColor = color;
                return;
            }
            var errorFound = false;
            for (int i = 0; i < count1; i++) {
                int val1 = parse1[i];
                int val2 = parse2[i];
                if (val1 != val2) {
                    Console.Error.WriteLine("Match Error ({0}|{1}): {2} x {3}",
                                    i, val1, val2);
                    errorFound = true;
                }
            }
            if (!errorFound) {
                Console.WriteLine("Results MATCH");
            }
        } // END Main

        private static readonly ConsoleColor color = Console.ForegroundColor;
        private static void WriteInfo(long elapsed = -1, int count = -1)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine();
            if (elapsed != -1 || count != -1) {
                Console.WriteLine("Test Info = > Ticks: {0} | Count: {1}",
                                elapsed, count);
            }
            Console.WriteLine("GC Info   => Total: {0} | MaxGen: {1}",
                            GC.GetTotalMemory(false), GC.MaxGeneration);
            Console.ForegroundColor = color;
        } // END WriteInfo

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
