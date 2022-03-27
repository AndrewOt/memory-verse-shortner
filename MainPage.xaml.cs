using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace MemoryVerseShortener
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            string inputText = input.Text;
            string transformedText = TransformString(inputText);
            output.Text = transformedText;
            DataPackage package = new DataPackage
            {
                RequestedOperation = DataPackageOperation.Copy,
            };
            package.SetText(transformedText);
            Clipboard.SetContent(package);
        }


        private string TransformString(string input)
        {
            string[] splitInput = input.Split(' ', '\n', '\r').Where(s => s.Length > 0).ToArray();
            Queue<string> outputList = new Queue<string>();

            // Add a number for verse 1 if no number is present
            if (!char.IsDigit(splitInput[0][0]))
            {
                outputList.Enqueue("1 ");
            }

            foreach (string s in splitInput)
            {
                //if (s.Length > 0)
                //{
                    if (char.IsDigit(s[0]))
                    {
                        outputList.Enqueue($"\n{s} ");
                    }
                    else
                    {
                        outputList.Enqueue(s.Substring(0, 1).ToUpper());
                    }

                    if (IsPunct(s, s.Length - 1))
                    {
                        outputList.Enqueue($"{s[s.Length - 1]} ");
                    }
                //}
            }

            StringBuilder outputString = new StringBuilder();
            while (outputList.Count > 0)
            {
                outputString.Append(outputList.Dequeue());
            }
            return outputString.ToString();
        }

        private bool IsPunct(string s, int index)
        {
            char c = s[index];
            switch (c)
            {
                case '.':
                case ',':
                case ':':
                case ';':
                case '-':
                case '!':
                case '?':
                    return true;
                default:
                    return false;
            }
        }
    }
}
