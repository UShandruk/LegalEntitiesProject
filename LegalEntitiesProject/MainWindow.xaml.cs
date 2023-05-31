using System;
using System.Collections.Generic;
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

namespace SpellChecker
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

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            string originalText = new TextRange(rtbOriginalText.Document.ContentStart, rtbOriginalText.Document.ContentEnd).Text.ToLower();
            
            string checkedText = TextChecker.GetCorrectedText(originalText);

            

            rtbCorrectedText.Document.Blocks.Clear();
            rtbCorrectedText.Document.Blocks.Add(new Paragraph(new Run(checkedText)));
        }
    }
}