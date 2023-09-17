using Autocomp.Nmea.Common;
using Autocomp.Nmea.Parser;
using Autocomp.Nmea.ParserLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace UnitTestsForParser
{
    [TestClass]
    public class NmeaParserTests
    {
        [TestMethod]
        public void TestParseGLLLatitude_ValidInput()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "5057.970", "N", "01424.326", "E", "123456.78", "A", "A" });
            double latitude = NmeaParser.ParseGLLLatitude(message);
            Assert.AreEqual(50.96616667, latitude, 0.000001); 
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseGLLLatitude_InvalidInput()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "invalid", "N", "01424.326", "E", "123456.78", "A", "A" });
            double latitude = NmeaParser.ParseGLLLatitude(message);
        }


        [TestMethod]
        public void TestParseGLLLongitude_ValidInput()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "5057.970", "N", "01424.326", "E", "123456.78", "A", "A" });
            double longitude = NmeaParser.ParseGLLLongitude(message);
            Assert.AreEqual(14.40543333, longitude, 0.000001); 
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseGLLLongitude_InvalidInput()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "5057.970", "N", "invalid", "E", "123456.78", "A", "A" });
            double longitude = NmeaParser.ParseGLLLongitude(message);
        }

        [TestMethod]
        public void TestParseGLLUTCTime_ValidInput()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "5057.970", "N", "01424.326", "E", "123456.78", "A", "A" });
            DateTime utcTime = NmeaParser.ParseGLLUTCTime(message);
            Assert.AreEqual(new DateTime(1, 1, 1, 12, 34, 56, 780), utcTime);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseGLLUTCTime_InvalidInput()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "5057.970", "N", "01424.326", "E", "invalid", "A", "A" });
            DateTime utcTime = NmeaParser.ParseGLLUTCTime(message);
        }
    }

    [TestClass]
    public class MWVParserTests
    {
        [TestMethod]
        public void TestParseWindAngle_ValidInput()
        {
            NmeaMessage message = new NmeaMessage("MWV", new[] { "270", "R", "10.5", "N", "A", "A" });
            double windAngle = MWVParser.ParseWindAngle(message);
            Assert.AreEqual(270.0, windAngle, 0.001); 
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseWindAngle_InvalidInput()
        {
            NmeaMessage message = new NmeaMessage("MWV", new[] { "invalid", "R", "10.5", "N", "A", "A" });
            double windAngle = MWVParser.ParseWindAngle(message);
        }

        [TestMethod]
        public void TestParseWindSpeed_ValidInput()
        {
            NmeaMessage message = new NmeaMessage("MWV", new[] { "270", "R", "10.5", "N", "A", "A" });
            double windSpeed = MWVParser.ParseWindSpeed(message);
            Assert.AreEqual(10.5, windSpeed, 0.001); 
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseWindSpeed_InvalidInput()
        {
            NmeaMessage message = new NmeaMessage("MWV", new[] { "270", "R", "invalid", "N", "A", "A" });
            double windSpeed = MWVParser.ParseWindSpeed(message);
        }
    }

    [TestClass]
    public class GLLParserTests
    {
        [TestMethod]
        public void TestParseLatitude_ValidInput()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "5057.970", "N", "01424.326", "E", "123456.78", "A", "A" });
            double latitude = GLLParser.ParseLatitude(message);
            Assert.AreEqual(50.96616667, latitude, 0.000001); 
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseLatitude_InvalidLatitude()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "invalid", "N", "01424.326", "E", "123456.78", "A", "A" });
            double latitude = GLLParser.ParseLatitude(message); 
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void TestParseLatitude_OutOfRange()
        {
            NmeaMessage message = new NmeaMessage("GLL", new[] { "9999.999", "N", "01424.326", "E", "123456.78", "A", "A" });
            double latitude = GLLParser.ParseLatitude(message); 
        }

    }

    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void DetermineSeparator_ValidCommaSeparator()
        {
            var mainWindow = new MainWindow();
            string nmeaSentence = "$GPGGA,123519,4807.038,N,01131.000,E,1,08,0.9,545.4,M,46.9,M,,*47";
            char separator = GetDetermineSeparator(mainWindow, nmeaSentence);
            Assert.AreEqual(',', separator);
        }

        [TestMethod]
        public void DetermineSeparator_ValidSemicolonSeparator()
        {
            var mainWindow = new MainWindow();
            string nmeaSentence = "$GPGGA;123519;4807.038;N;01131.000;E;1;08;0.9;545.4;M;46.9;M;;*47";
            char separator = GetDetermineSeparator(mainWindow, nmeaSentence);
            Assert.AreEqual(';', separator);
        }

        [TestMethod]
        public void DetermineSeparator_ValidColonSeparator()
        {
            var mainWindow = new MainWindow();
            string nmeaSentence = "$GPGGA:123519:4807.038:N:01131.000:E:1:08:0.9:545.4:M:46.9:M::*47";
            char separator = GetDetermineSeparator(mainWindow, nmeaSentence);
            Assert.AreEqual(':', separator);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void DetermineSeparator_InvalidSeparator()
        {
            var mainWindow = new MainWindow();
            string nmeaSentence = "$GPGGA?123519?4807.038?N?01131.000?E?1?08?0.9?545.4?M?46.9?M??*47";
            char separator = GetDetermineSeparator(mainWindow, nmeaSentence);
        }

        private char GetDetermineSeparator(MainWindow mainWindow, string nmeaSentence)
        {
            MethodInfo methodInfo = typeof(MainWindow).GetMethod("DetermineSeparator", BindingFlags.NonPublic | BindingFlags.Instance);
            return (char)methodInfo.Invoke(mainWindow, new object[] { nmeaSentence });
        }
    }

}

