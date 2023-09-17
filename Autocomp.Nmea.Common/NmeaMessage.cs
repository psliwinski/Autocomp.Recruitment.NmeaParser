using System;
using System.Text;

namespace Autocomp.Nmea.Common
{
    public class NmeaMessage
    {
        public string Header { get; set; }

        public string[] Fields { get; set; }

        public NmeaFormat Format { get; set; } = NmeaFormat.Default;

        private static StringBuilder text = new StringBuilder();

        public NmeaMessage(string header, string[] fields)
            : this(header, fields, NmeaFormat.Default)
        {
        }

        public NmeaMessage(string header, string[] fields, NmeaFormat format)
        {
            this.Header = header;
            this.Fields = fields;
            this.Format = format;
        }

        public NmeaMessage(string body, char separator)
        {
            string[] parts = body.Split(new char[] { separator });
            if (parts.Length >= 1)
            {
                Header = parts[0];
                Fields = new string[parts.Length - 1];
                for (int i = 1; i < parts.Length; i++)
                {
                    Fields[i - 1] = parts[i];
                }
            }
        }

        public NmeaMessage(string body)
            : this(body, NmeaFormat.DefaultSeparator) { }

        public static NmeaMessage FromString(string msg)
        {
            return FromString(msg, NmeaFormat.Default);
        }

        public static NmeaMessage FromString(string msg, NmeaFormat format)
        {
            return new NmeaMessage(msg.TrimStart(format.Prefix).Remove(msg.IndexOf(format.Suffix) - 1));
        }

        /// <summary>Tworzy łańcuch z komunikatem NMEA wg podanego formatu</summary>
        /// <param name="msg">Komunikat NMEA</param>
        /// <param name="format">Definicja sposobu formatowania komunikatu</param>
        /// <returns>Sformatowany komunikat NMEA</returns>
        public static string ToString(NmeaMessage msg, NmeaFormat format)
        {
            if (msg == null)
            {
                throw new ArgumentNullException();
            }

            lock (text)
            {
                text.Remove(0, text.Length);

                text.Append(format.Prefix);
                if (!string.IsNullOrEmpty(msg.Header))
                {
                    text.Append(msg.Header);
                }

                if (msg.Fields != null)
                {
                    foreach (string f in msg.Fields)
                    {
                        text.Append(format.Separator);
                        if (!string.IsNullOrEmpty(f))
                        {
                            text.Append(f);
                        }
                    }
                }

                text.Append(format.Suffix);

                byte crc = NmeaCrcCalculator.CRC(msg, format);
                text.Append(crc.ToString("X02"));

                if (!string.IsNullOrEmpty(format.Terminator))
                {
                    text.Append(format.Terminator);
                }

                return text.ToString();
            }
        }

        public override string ToString()
        {
            return ToString(this, Format);
        }
    }
}
