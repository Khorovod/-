using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Калькулятор
{
    public partial class Calculator : Form
    {
        public char mathButton;
        public string firstDigitsInTextbox;
        public bool firstDigitEntered;
        public bool secondDigitTurn;

        string TText
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }


        public Calculator()
        {
            firstDigitEntered = false;
            secondDigitTurn = false;
            InitializeComponent();
        }

        private void Calculator_Load(object sender, EventArgs e)
        {
            Console.Beep();
            Console.Beep();
            Console.Beep();
        }

        #region Обработчики событий
        void FocusTextBox(object sender, EventArgs e)
        {
            textBox.Select();
            textBox.SelectionStart = TText.Length;
        }

        private void DigitsButtonClick(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (firstDigitEntered)
            {
                firstDigitEntered = false;
                secondDigitTurn = true;
                TText = "";
            }
            if (TText == "0")
            {
                TText = button.Text;
            }
            else
            {
                TText += button.Text;
            }
        }
        private void KeyPressed(object sender, KeyPressEventArgs e)
        {
            var plus = (char)43;
            var minus = (char)45;
            var mult = (char)42;
            var divd = (char)47;

            var test = e.KeyChar;

            if (char.IsDigit(e.KeyChar))
            {
                if (firstDigitEntered)
                {
                    firstDigitEntered = false;
                    secondDigitTurn = true;
                    TText = "";
                }
                if (TText == "0")
                {
                    TText = TText.Trim('0');
                }
            }
            else if (e.KeyChar == plus || e.KeyChar == minus || e.KeyChar == mult || e.KeyChar == divd) 
            {
                firstDigitsInTextbox = TText.Trim('=');
                firstDigitEntered = true;
                mathButton = e.KeyChar;

                TText = firstDigitsInTextbox + mathButton ;
                //чтобы не выводились символы в тексовое поле

                e.Handled = true;
            }
            else if(e.KeyChar == (char)8)
            {
                //DeleteLastDigitClick(sender, e);
                if(TText != "")
                {
                    textBox.Text = textBox.Text.Substring(0, TText.Length - 1);
                }

                e.Handled = true;
            }
            else if (e.KeyChar == (char)13 || e.KeyChar == '=')
            {
                if (secondDigitTurn)
                {
                    //если после первой цифры или сразу нажимать - баг
                    DoMathInputedEquation();
                    e.Handled = true;
                }
            }
            else if(e.KeyChar == '%')
            {
                CalcPersent();
                e.Handled = true;
            }
            else if (e.KeyChar == '^')
            {
                PowerTwoButtontClick(sender, e);
                e.Handled = true;
            }
            else if (e.KeyChar == ',' || e.KeyChar == '.')
            {
                if (!TText.Contains(","))
                {
                    TText += ",";
                }
                e.Handled = true;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void MathButtonClick(object sender, EventArgs e)
        {
            var b = (Button)sender;

            mathButton = b.Text.ToCharArray()[0];
            firstDigitsInTextbox = TText.Trim('=');

            TText += mathButton;

            firstDigitEntered = true;
        }

        private void EqualSignButton(object sender, EventArgs e)
        {
            if (secondDigitTurn)
            {
                DoMathInputedEquation();
            }
        }

        private void SquareButtonClick(object sender, EventArgs e)
        {
            TText = $"={Math.Sqrt(Convert.ToDouble(TText.Trim('=')))}";
            firstDigitEntered = false;
        }

        private void PowerTwoButtontClick(object sender, EventArgs e)
        {
            TText = $"={Math.Pow(Convert.ToDouble(TText.Trim('=')), 2)}";
            firstDigitEntered = false;
        }

        private void DeleteLastDigitClick(object sender, EventArgs e)
        {
            textBox.Text = textBox.Text.Substring(0, TText.Length - 1);
            if(TText == "")
            {
                TText = "0";
            }
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            TText = "0";
            textBox.Select();
            textBox.SelectionStart = TText.Length;
        }

        private void CommaClick(object sender, EventArgs e)
        {
            if (!TText.Contains(","))
            {
                TText += ",";
            }
            textBox.Select();
            textBox.SelectionStart = TText.Length;
        }
 
        private void MinusClick(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox.Text.Trim('='));

            if (!TText.Contains("-"))
            {
                textBox.Text = "-" + textBox.Text.Trim('=');
            }
            else if (TText.Contains("-"))
            {
                TText = Math.Abs(x).ToString();
            }
            textBox.Select();
            textBox.SelectionStart = TText.Length;
        }

        private void TextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TText = "Хватит кликать!";
        }

        private void PersentButtonClick(object sender, EventArgs e)
        {
            CalcPersent();
        }

        #endregion

        public void DoMathInputedEquation()
        {
            double firstDigit, secDigit, result = 0;
            try
            {
                firstDigit = Convert.ToDouble(firstDigitsInTextbox);
                secDigit = Convert.ToDouble(TText);
                switch (mathButton)
                {
                    case '+':
                        result = firstDigit + secDigit;
                        break;
                    case '-':
                        result = firstDigit - secDigit;
                        break;
                    case '*':
                        result = firstDigit * secDigit;
                        break;
                    case '/':
                        result = firstDigit / secDigit;
                        break;
                    default:

                        break;
                }

                TText = $"= {result}";
            }
            catch(Exception)
            {
                TText = "Ну вот, все сломалось...";
            }
            finally
            {
                firstDigitEntered = false;
                secondDigitTurn = false;
                textBox.Select();
                textBox.SelectionStart = TText.Length;
            }
        }

        private void CalcPersent()
        {
            double firstDigit, secDigit, result = 0;
            try
            {
                if (firstDigitsInTextbox != null)
                {
                    firstDigit = Convert.ToDouble(firstDigitsInTextbox.Trim('='));
                    secDigit = Convert.ToDouble(TText.Trim('='));
                    switch (mathButton)
                    {
                        case '+':
                            result = firstDigit + (firstDigit * secDigit / 100);
                            break;
                        case '-':
                            result = firstDigit - (firstDigit * secDigit / 100);
                            break;
                        case '*':
                            result = firstDigit * (firstDigit * secDigit / 100);
                            break;
                        case '/':
                            result = firstDigit / (firstDigit * secDigit / 100);
                            break;
                        default:
                            break;
                    }
                    textBox.Text = $"= {result}";
                }
            }
            catch
            {
                TText = "Проценты как то не так посчитались...";
            }
            finally
            {
                firstDigitEntered = false;
                secondDigitTurn = false;
                textBox.Select();
                textBox.SelectionStart = TText.Length;
            }
        }

    }
}
