using Autocomp.Nmea.Common;
using System;
using System.Globalization;

namespace Autocomp.Nmea.ParserLib
{
    public static class GLLParser
    {
        public static double ParseLatitude(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 1)
            {
                string degreesStr = message.Fields[0].Substring(0, 2);
                string minutesStr = message.Fields[0].Substring(2);

                if (double.TryParse(degreesStr, out double degrees) && double.TryParse(minutesStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double minutes))
                {
                    double latitude = degrees + (minutes / 60.0);

                    if (latitude >= -90 && latitude <= 90)
                    {
                        return latitude;
                    }
                    else
                    {
                        throw new ArgumentException("Latitude is out of range.");
                    }

                }
                else
                {
                    throw new FormatException("Latitude is not a valid double.");
                }


            }
            throw new FormatException("GLL sentence does not contain latitude.");
        }

        public static string ParseNsIndicator(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 2)
            {
                string nsIndicator = message.Fields[1];

                if (nsIndicator == "N" || nsIndicator == "S")
                {
                    return nsIndicator;
                }
                else
                {
                    throw new ArgumentException("N/S Indicator is not a valid value.");
                }
            }
            throw new FormatException("GLL sentence does not contain N/S Indicator.");
        }

        public static double ParseLongitude(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 3)
            {
                string degreesStr = message.Fields[2].Substring(0, 3);
                string minutesStr = message.Fields[2].Substring(3);

                if (double.TryParse(degreesStr, out double degrees) && double.TryParse(minutesStr, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double minutes))
                {
                    double longitude = degrees + (minutes / 60.0);

                    if (longitude >= -180 && longitude <= 180)
                    {
                        return longitude;
                    }
                    else
                    {
                        throw new ArgumentException("Longitude is out of range.");
                    }
                }
                else
                {
                    throw new FormatException("Longitude is not a valid double.");
                }
            }
            throw new FormatException("GLL sentence does not contain longitude.");
        }

        public static string ParseEwIndicator(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 4)
            {
                string ewIndicator = message.Fields[3];

                if (ewIndicator == "E" || ewIndicator == "W")
                {
                    return ewIndicator;
                }
                else
                {
                    throw new ArgumentException("E/W Indicator is not a valid value.");
                }
            }
            throw new FormatException("GLL sentence does not contain E/W Indicator.");
        }

        public static DateTime ParseUTCTime(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 5)
            {
                string utcTimeStr = message.Fields[4];

                if (utcTimeStr.Length == 9)
                {
                    try
                    {
                        DateTime utcTime = DateTime.ParseExact(utcTimeStr, "HHmmss.ff", CultureInfo.InvariantCulture);
                        return utcTime;
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException(ex.Message);
                    }
                }
                throw new FormatException("UTC time length is not correct.");
            }
            throw new FormatException("GLL sentence does not contain UTC time.");
        }

        public static string ParseStatus(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 6)
            {
                string status = message.Fields[5];

                if (status == "A" || status == "V")
                {
                    return status;
                }
                else
                {
                    throw new ArgumentException("Status is not a valid value.");
                }
            }
            throw new FormatException("GLL sentence does not contain status.");
        }

        public static string ParseModeIndicator(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 7)
            {
                string modeIndicator = message.Fields[6];

                if (modeIndicator == "A" || modeIndicator == "D" || modeIndicator == "E" || modeIndicator == "N" || modeIndicator == "S")
                {
                    return modeIndicator;
                }
                else
                {
                    throw new ArgumentException("Mode Indicator is not a valid value.");
                }
            }
            throw new FormatException("GLL sentence does not contain Mode Indicator.");
        }
    }

    public static class MWVParser
    {
        public static double ParseWindAngle(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 1)
            {
                if (double.TryParse(message.Fields[0], out double windAngle))
                {
                    if (windAngle >= 0 && windAngle < 360)
                    {
                        return windAngle;
                    }
                    else
                    {
                        throw new ArgumentException("Wind angle is out of range.");
                    }
                }
                else
                {
                    throw new FormatException("Wind angle is not a valid double.");
                }
            }
            throw new FormatException("MWV sentence does not contain wind angle.");
        }

        public static string ParseReference(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 2)
            {
                string reference = message.Fields[1];

                if (reference == "R" || reference == "T")
                {
                    return reference;
                }
                else
                {
                    throw new ArgumentException("Reference is not a valid value.");
                }
            }
            throw new FormatException("MWV sentence does not contain reference.");
        }

        public static double ParseWindSpeed(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 3)
            {
                if (double.TryParse(message.Fields[2], out double windSpeed))
                {
                    if (windSpeed >= 0 && windSpeed <= 100.0)
                    {
                        return windSpeed;
                    }
                    else
                    {
                        throw new ArgumentException("Wind speed is out of range.");
                    }
                }
                else
                {
                    throw new FormatException("Wind speed is not a valid double.");
                }
            }
            throw new FormatException("MWV sentence does not contain wind speed.");
        }

        public static string ParseWindSpeedUnits(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 4)
            {
                string windSpeedUnits = message.Fields[3];

                if (windSpeedUnits == "K" || windSpeedUnits == "M" || windSpeedUnits == "N" || windSpeedUnits == "S")
                {
                    return windSpeedUnits;
                }
                else
                {
                    throw new ArgumentException("Wind Speed Units is not a valid value.");
                }
            }
            throw new FormatException("MWV sentence does not contain Wind Speed Units.");
        }

        public static string ParseStatus(NmeaMessage message)
        {
            if (message.Fields != null && message.Fields.Length >= 5)
            {
                string status = message.Fields[4];

                if (status == "A" || status == "V")
                {
                    return status;
                }
                else
                {
                    throw new ArgumentException("Status is not a valid value.");
                }
            }
            throw new FormatException("MWV sentence does not contain status.");
        }
    }
}