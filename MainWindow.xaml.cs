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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string? previousNumber;
        public string? operation;
        public string? previousAnswer;

        public MainWindow()
        {
            InitializeComponent();
            but0.Click += but_Click;
            but1.Click += but_Click;
            but2.Click += but_Click;
            but3.Click += but_Click;
            but4.Click += but_Click;
            but5.Click += but_Click;
            but6.Click += but_Click;
            but7.Click += but_Click;
            but8.Click += but_Click;
            but9.Click += but_Click;
            butDiv.Click += but_Click;
            butMul.Click += but_Click;
            butDec.Click += but_Click;
            butPlus.Click += but_Click;
            butMinus.Click += but_Click;
            butPow.Click += but_Click;
            butNeg.Click += but_Click;
            butEqu.Click += but_Click;
            butDel.Click += but_Click;
        }
        public void addText(string? x)
        {
            if (Answer.Text == "")
            {
                Answer.Text = x;
            }
            else
            {
                Answer.Text = $"{Answer.Text}{x}";
            }
        }

        private void but_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (previousNumber == null)
            {
                bool success = int.TryParse(btn.Content.ToString(), out int num);
                if (success)
                {
                    addText(btn.Content.ToString());
                }
                else
                {
                    switch (btn.Content.ToString())
                    {
                        case "del":
                            if (Answer.Text != "")
                            {
                                Answer.Text = Answer.Text.Remove(Answer.Text.Length - 1, 1);
                            }
                            break;
                        case "+/-":
                            if (Answer.Text != "")
                            {
                                if (double.TryParse(Answer.Text, out double value))
                                {
                                    value *= -1;
                                    Answer.Text = value.ToString();
                                }
                            }
                            break;
                        case ".":
                            if (!Answer.Text.Contains('.'))
                            {
                                Answer.Text = $"{Answer.Text}.";
                            }
                            break;
                        case "=":
                            break;
                        default:
                            if (Answer.Text != "")
                            {
                                previousNumber = Answer.Text == "-" ? "-1" : Answer.Text;
                                operation = btn.Content.ToString();
                                Answer.Text = btn.Content.ToString();
                            }
                            break;
                    }
                }
            }
            else if (btn.Content.ToString() == "=" && operation != null && Answer.Text != "")
            {
                double.TryParse(Answer.Text, out double nextNum);
                double.TryParse(previousNumber, out double prevNum);
                switch (operation)
                {
                    case "/":
                        Answer.Text = (prevNum / nextNum).ToString();
                        break;
                    case "+":
                        Answer.Text = (prevNum + nextNum).ToString();
                        break;
                    case "-":
                        Answer.Text = (prevNum - nextNum).ToString();
                        break;
                    case "*":
                        Answer.Text = (prevNum * nextNum).ToString();
                        break;
                    case "^":
                        Answer.Text = (Math.Pow(prevNum, nextNum)).ToString();
                        break;
                }
                previousNumber = Answer.Text;
            }
            else if (Answer.Text != "" && previousNumber != null)
            {
                string comp = btn.Content.ToString();
                if (comp == "^" || comp == "+" || comp == "-" || comp == "/" || comp == "*")
                {
                    Answer.Text = comp;
                    operation = comp;
                }
                else if (double.TryParse(comp, out double x) || comp == "+/-" || comp == "del" || comp == ".")
                {
                    bool success = double.TryParse(Answer.Text, out double num);
                    if (!success)
                    {
                        Answer.Text = comp;
                    }
                    else
                    {
                        if (comp == "del")
                        {
                            Answer.Text = Answer.Text.Remove(Answer.Text.Length - 1, 1);
                        }
                        else if (comp == "+/-")
                        {
                            num *= -1;
                            Answer.Text = num.ToString();
                        }
                        else if (comp == "." && !Answer.Text.Contains('.'))
                        {
                            Answer.Text = $"{Answer.Text}.";
                        }
                        else
                        {
                            addText(comp);
                        }
                    }
                }
            }
        }
    }
}
