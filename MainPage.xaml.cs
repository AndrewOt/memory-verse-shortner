using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using System.Collections.Generic;
using System.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MemoryVerseShortener
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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


        public string TransformString(string input)
        {
            string[] splitInput = input.Split(' ');
            Queue<string> outputList = new Queue<string>();

            // Add a number for verse 1 if no number is present
           if (!char.IsDigit(splitInput[0][0]))
            {
                outputList.Enqueue("1 ");
            }

            foreach (string s in splitInput)
            {
                if (char.IsDigit(s[0]))
                {
                    outputList.Enqueue($"\n{s} ");
                }
                else
                {
                    outputList.Enqueue(s.Substring(0, 1).ToUpper());
                }

                if (char.IsPunctuation(s, s.Length -1))
                {
                    outputList.Enqueue($"{s[s.Length - 1]} ");
                }
            }

            StringBuilder outputString = new StringBuilder();
            while (outputList.Count > 0)
            {
                outputString.Append(outputList.Dequeue());
            }
            return outputString.ToString();
        }
    }
}
