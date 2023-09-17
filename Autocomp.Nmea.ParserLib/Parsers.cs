using Autocomp.Nmea.Common;
using System;
using System.Globalization;

namespace Autocomp.Nmea.ParserLib
{
    public static class GLLParser
    {
        public static string ParseLatitude(NmeaMessage message)
        {
            string latitude = message.Fields[0];
            return latitude;
        }

        public static string ParseNsIndicator(NmeaMessage message)
        {
            string nsIndicator = message.Fields[1];
            return nsIndicator;
        }

        public static string ParseLongitude(NmeaMessage message)
        {
            string longitude = message.Fields[2];
            return longitude;
        }

        public static string ParseEwIndicator(NmeaMessage message)
        {
            string ewIndicator = message.Fields[3];
            return ewIndicator;
        }

        public static string ParseUTCTime(NmeaMessage message)
        {
            string utcTime = message.Fields[4];
            return utcTime;
        }

        public static string ParseStatus(NmeaMessage message)
        {
            string status = message.Fields[5];
            return status;
        }

        public static string ParseModeIndicator(NmeaMessage message)
        {
            string modeIndicator = message.Fields[6];
            return modeIndicator;

        }

    }
}