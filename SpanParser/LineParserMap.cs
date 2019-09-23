using System;
using System.Collections.Generic;

namespace SpanParser
{
    public class LineParserMap
    {
        private readonly Dictionary<int, ILineParser> map;
        private readonly ILineParser std;

        public LineParserMap(ILineParser[] lineParsers, ILineParser defaultParser)
        {
            if (defaultParser == null) {
                throw new ArgumentNullException("Default Parser REQUIERED");
            }
            if (lineParsers == null || lineParsers.Length == 0) {
                return;
            }
            map = new Dictionary<int, ILineParser>();
            std = defaultParser;
            for (int i = 0; i < lineParsers.Length; i++) {
                var parser = lineParsers[i];
                map[parser.Match] = parser;
            }
        }
        public LineParserMap(ILineParser defaultParser)
        {
            if (defaultParser == null) {
                throw new ArgumentNullException("Default Parser REQUIERED");
            }
            std = defaultParser;
        }

        public void Parse(ReadOnlySpan<char> lineData)
        {
        } // END Parse
    }
}
