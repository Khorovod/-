using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Калькулятор
{
    public partial class Form1 : Form
    {
        public string mathButton;
        public string firstDigitsInTextbox;
        public bool secondDigitGonnaEntered;
        public bool isSecondDigitEntered;

        public Form1()
        {
            secondDigitGonnaEntered = false;
            isSecondDigitEntered = false;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.Beep();
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void DigitsButtonClick(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (secondDigitGonnaEntered)
            {
                secondDigitGonnaEntered = false;
                isSecondDigitEntered = true;
                textBox1.Text = "";
            }
            if (textBox1.Text == "0")
            {
                textBox1.Text = b.Text;
            }
            else
            {
                textBox1.Text = textBox1.Text + b.Text;
            }
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void MathButtonClick(object sender, EventArgs e)
        {
            TakeFirstDigitAndSign(sender);

            secondDigitGonnaEntered = true;
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void EqualSignButton(object sender, EventArgs e)
        {
            //if (textBox1.Text.Contains("+") || textBox1.Text.Contains("-") || textBox1.Text.Contains("*") || textBox1.Text.Contains("/"))

            try
                {
                if (isSecondDigitEntered)
                {
                    DoMathInputedEquation();
                    secondDigitGonnaEntered = false;
                    isSecondDigitEntered = false;
                }

                }
            catch
                {
                    textBox1.Text = "Ну вот, все сломалось...";
                }
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void SquareButtonClick(object sender, EventArgs e)
        {
            textBox1.Text = $"={Math.Sqrt(Convert.ToDouble(textBox1.Text.Trim('='))).ToString()}";
            secondDigitGonnaEntered = false;
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void PowerTwoButtontClick(object sender, EventArgs e)
        {
            textBox1.Text = $"={Math.Pow(Convert.ToDouble(textBox1.Text.Trim('=')), 2).ToString()}";
            secondDigitGonnaEntered = false;
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void DeleteLastDigitClick(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            if(textBox1.Text == "")
            {
                textBox1.Text = "0";
            }
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void CommaClick(object sender, EventArgs e)
        {
            if (!textBox1.Text.Contains(","))
            {
                textBox1.Text = textBox1.Text + ",";
            }
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }
 
        private void MinusClick(object sender, EventArgs e)
        {
            double x = Convert.ToDouble(textBox1.Text.Trim('='));

            if (!textBox1.Text.Contains("-"))
            {
                textBox1.Text = "-" + textBox1.Text.Trim('=');
            }
            else if (textBox1.Text.Contains("-"))
            {
                //textBox1.Text = textBox1.Text.Trim('-');
                textBox1.Text = Math.Abs(x).ToString();
            }
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void TextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = "Хватит кликать!";
        }

        public void DoMathInputedEquation()
        {
            double firstDigit, secDigit, result;
            result = 0;//можно нажать равно до ввода дейтсвия
            firstDigit = Convert.ToDouble(firstDigitsInTextbox.Trim('='));
            secDigit = Convert.ToDouble(textBox1.Text);
            if (mathButton == "+")
            {
                result = firstDigit + secDigit;
            }
            if (mathButton == "-")
            {
                result = firstDigit - secDigit;
            }
            if (mathButton == "*")
            {
                result = firstDigit * secDigit;
            }
            if (mathButton == "/")
            {
                result = firstDigit / secDigit;
            }
            textBox1.Text = $"={ result.ToString()}";
            textBox1.Select();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void PersentButtonClick(object sender, EventArgs e)
        {
            CalcPersent();
        }

        private void TextBoxNumberic_KeyPress(object sender, KeyPressEventArgs e)
        {

            const char Delete = (char)8;
            const char Back = (char)Keys.Back;

            e.Handled = !Char.IsDigit(e.KeyChar) && e.KeyChar != Delete &&
                e.KeyChar != '%' && e.KeyChar != Back && e.KeyChar != ',';

        }
        //не работает с вводом цифр с клавы
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //if(e.KeyChar >= '0' && e.KeyChar <= '9')
            //{

            //}

            if (e.KeyData == Keys.Add)
            {
                TextBox b = (TextBox)sender;
                mathButton = "+";
                firstDigitsInTextbox = textBox1.Text.Trim('=');
                secondDigitGonnaEntered = true;
            }
            if (e.KeyData == Keys.Subtract)
            {
                TextBox b = (TextBox)sender;
                mathButton = "-";
                firstDigitsInTextbox = textBox1.Text.Trim('=');
                secondDigitGonnaEntered = true;
            }
            if (e.KeyData == Keys.Multiply)
            {
                TextBox b = (TextBox)sender;
                mathButton = "*";
                firstDigitsInTextbox = textBox1.Text.Trim('=');
                secondDigitGonnaEntered = true;
            }
            if (e.KeyData == Keys.Divide)
            {
                TextBox b = (TextBox)sender;
                mathButton = "/";
                firstDigitsInTextbox = textBox1.Text.Trim('=');
                secondDigitGonnaEntered = true;
            }
            if(e.KeyData == Keys.Enter)
            {
                DoMathInputedEquation();
            }

        }

        private void CalcPersent()
        {
            double firstDigit, secDigit, result;
            result = 0;//можно нажать равно до ввода дейтсвия
            if(firstDigitsInTextbox != null)
            {
                firstDigit = Convert.ToDouble(firstDigitsInTextbox.Trim('='));
                secDigit = Convert.ToDouble(textBox1.Text.Trim('='));
                if (mathButton == "+")
                {
                    result = firstDigit + (firstDigit * secDigit / 100);
                }
                if (mathButton == "-")
                {
                    result = firstDigit - (firstDigit * secDigit / 100);
                }
                if (mathButton == "*")
                {
                    result = firstDigit * (firstDigit * secDigit / 100);
                }
                if (mathButton == "/")
                {
                    result = firstDigit / (firstDigit * secDigit / 100);
                }
                textBox1.Text = $"={ result.ToString()}";
                textBox1.Select();
                textBox1.SelectionStart = textBox1.Text.Length;
            }

        }

        private void TakeFirstDigitAndSign(object sender)
        {
            Button b = (Button)sender;
            mathButton = b.Text;
            firstDigitsInTextbox = textBox1.Text.Trim('=');
        }

    }
}
