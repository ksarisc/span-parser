using System;
using System.Buffers;
using System.IO;
using System.Text;

namespace SpanParser
{
    public class TextFileReader
    {
        private static readonly byte carriageReturn = 13;
        private static readonly byte newLine = 10;

        private readonly LineParserMap parsers;
        public void Read(string fileName)
        {
            var sb = new StringBuilder();
            var pool = ArrayPool<char>.Shared;
            using (var rdr = File.OpenRead(fileName))
            {
                var atEnd = false;
                try
                {
                    while (rdr.CanRead)
                    {
                        sb.Clear();
                        while (!atEnd)
                        {
                            var curbyte = rdr.ReadByte();
                            if (curbyte == -1) {
                                atEnd = true;
                                break;
                            }
                            if (curbyte == carriageReturn) {
                                continue;
                            }
                            if (curbyte == newLine) {
                                break;
                            }
                            
                            sb.Append((char)curbyte);
                        }

                        if (atEnd) {
                            break;
                        }

                        int length = sb.Length;
                        var buffer = pool.Rent(length);
                        try {
                            for (int i = 0; i < length; i++) {
                                buffer[i] = sb[i];
                            }

                            parsers.Parse(buffer);
                        } catch (Exception eLine) {
                            throw;
                        } finally {
                            pool.Return(buffer, true);
                        }
                    }
                } catch (Exception eFile) {
                    throw;
                }
            }
        } // END Read
    }
}
