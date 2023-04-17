using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace OgreBattleNameEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<NameRecord> NameRecords;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = "Super Nintendo Rom"; // Default file name
            dialog.DefaultExt = ".smc"; // Default file extension
            dialog.Filter = "SMC ROM(.smc)|*.smc"; // Filter files by extension

            // Show open file dialog box
            bool? result = dialog.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string fileName = dialog.FileName;
                byte[] fileBytes = File.ReadAllBytes(fileName);
                FileNameTextBox.Text = fileName;
                NameProcessor nameProcessor = new NameProcessor();
                NameRecords = nameProcessor.processNameRecords(fileBytes);
            }
            this.namesGrid.ItemsSource = NameRecords;
        }
    }
}
