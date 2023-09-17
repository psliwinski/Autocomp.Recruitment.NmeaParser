using Autocomp.Nmea.Common;
using Autocomp.Nmea.ParserLib;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Autocomp.Nmea.Parser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ParseButton_Click(object sender, RoutedEventArgs e)
        {
            ParsedValuesListView.Items.Clear();
            ErrorTextBlock.Text = string.Empty;

            try
            {
                string nmeaSentence = NmeaSentenceTextBox.Text;
                char separator = DetermineSeparator(nmeaSentence);

                NmeaMessage nmeaMessage = NmeaParser.Parse(nmeaSentence, separator);

                if (nmeaMessage != null)
                {
                    
                    foreach (string fieldValue in nmeaMessage.Fields)
                    {
                        ListViewItem fieldItem = new ListViewItem();
                        fieldItem.Content = fieldValue;
                        ParsedValuesListView.Items.Add(fieldItem);
                        
                    }

                    if (!string.IsNullOrEmpty(nmeaMessage.Checksum))
                    {
                        ListViewItem checksumItem = new ListViewItem();
                        checksumItem.Content = nmeaMessage.Checksum;
                        ParsedValuesListView.Items.Add(checksumItem);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTextBlock.Text = $"Error parsing NMEA sentence: {ex.Message}";
            }
        }

        private char DetermineSeparator(string nmeaSentence)
        {
            char[] possibleSeparators = { ',', ';', ':' }; 
            foreach (char separator in possibleSeparators)
            {
                if (nmeaSentence.Contains(separator))
                {
                    return separator;
                }
            }
            
            throw new FormatException("Unable to determine the separator in the NMEA sentence.");
        }
    }
}

