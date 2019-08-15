using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Xml.Linq;
using System.Xml.XPath;

namespace Wetenschappelijke_Rekenmachine
{
    /// <summary>
    /// Interaction logic for Calculator.xaml
    /// </summary>
    public partial class Calculator : Window
    {
        public Calculator()
        {
            InitializeComponent();
            this.Textbox.Text = "0";
        }

        int OperatorCount = 0;
        int ClearEntry = 0;
        int MemoryCount = 0;
        int PercentageCount = 0;

        double Number1 = 0.0;
        double Number2 = 0.0;
        double HistoryNumber = 0.0;
        double MemoryStoreNumber = 0.0;
        double MemoryRecall = 0.0;
        double Result = 0.0;

        string Input = string.Empty;
        string NextInput = string.Empty;
        string Operators = string.Empty;
        string OperatorDetected = string.Empty;
        string Detected = string.Empty;
        string RealOperator = string.Empty;
        string FirstOperator = string.Empty;
        string PreviousOperator = string.Empty;
        string CalculationString = string.Empty;

        List<string> OperatorsArray = new List<string>();
        List<double> MemoryArray = new List<double>();
        List<double> MemoryStoreArray = new List<double>();
        List<string> MemoryStoreShowArray = new List<string>();

        bool XYClicked = false;

        string EquallityInTheMeantime()
        {
            if (OperatorCount > 2)
            {
                switch (PreviousOperator)
                {
                    case "+":
                        Result += Number2;
                        break;
                    case "-":
                        Result -= Number2;
                        break;
                    case "x":
                        Result *= Number2;
                        break;
                    case "÷":
                        Result /= Number2;
                        break;
                }
            }

            if (OperatorCount == 2)
            {
                switch (FirstOperator)
                {
                    case "+":
                        Result = Number1 + Number2;
                        break;
                    case "-":
                        Result = Number1 - Number2;
                        break;
                    case "x":
                        Result = Number1 * Number2;
                        break;
                    case "÷":
                        Result = Number1 / Number2;
                        break;
                }
            }

            this.Textbox.Text = Result.ToString();
            return this.Textbox.Text;
        }
        private void Clear_Entry(object sender, RoutedEventArgs e)
        {
            if (this.Textbox.Text.Length == 0 || this.Textbox.Text.Length == 1)
            {
                this.Textbox.Text = "0";
                NextInput = string.Empty;
            }
            else
            {
                this.Textbox.Text = this.Textbox.Text.Remove(0, ClearEntry);
                this.Textbox.Text = "0";
                NextInput = string.Empty;
            }
        }
        private void Clear(object sender, RoutedEventArgs e)
        {
            this.Textbox.Text = "0";
            this.LabelTextBox.Text = "";
            OperatorCount = 0;
            ClearEntry = 0;
            Number1 = 0.0;
            Number2 = 0.0;
            Result = 0.0;
            NextInput = string.Empty;
            Operators = string.Empty;
            Input = string.Empty;
            OperatorDetected = string.Empty;
            Detected = string.Empty;
            RealOperator = string.Empty;
            FirstOperator = string.Empty;
            PreviousOperator = string.Empty;
            CalculationString = string.Empty;
        }

        private void Backspace(object sender, RoutedEventArgs e)
        {
            this.Textbox.Text = this.Textbox.Text.Remove(this.Textbox.Text.Length - 1);
            if (this.Textbox.Text.Length == 0)
            {
                this.Textbox.Text = "0";
            }

            Input = string.Empty;
            Operators = string.Empty;
        }

        public void ClickedNumber(object sender, RoutedEventArgs e)
        {
            if (OperatorCount == 0)
            {
                this.Textbox.Text = "";
                Button Detected = (Button)sender;
                Input += Detected.Content.ToString();
                Number1 = Convert.ToDouble(Input);
                this.Textbox.Text = Input;
            }

            if (OperatorCount >= 1)
            {
                Button Detected = (Button)sender;
                NextInput += Detected.Content.ToString();
                ClearEntry = NextInput.Length;
                Number2 = Convert.ToDouble(NextInput);
                this.Textbox.Text = NextInput;
            }
        }

        public void Operator_Clicked(object sender, RoutedEventArgs e)
        {
            this.LabelTextBox.Text = CalculationString;
            OperatorCount++;
            this.LabelTextBox.Text += " " + NextInput;
            CalculationString = this.LabelTextBox.Text;
            Button Operators = (Button)sender;
            string OperatorDetected = Operators.Content.ToString();
            RealOperator = OperatorDetected;
            OperatorsArray.Add(RealOperator);

            if (OperatorCount == 1)
            {
                this.LabelTextBox.Text = this.Textbox.Text;
                FirstOperator = RealOperator;
                this.LabelTextBox.Text = this.Textbox.Text + " " + OperatorDetected;
                CalculationString = this.LabelTextBox.Text;
            }

            if (OperatorCount > 1)
            {
                for (int i = OperatorsArray.Count - 1; i < OperatorsArray.Count; i++)
                {
                    PreviousOperator = OperatorsArray[i - 1];
                }

                this.LabelTextBox.Text = CalculationString + " " + OperatorDetected;
                EquallityInTheMeantime();
            }

            NextInput = "";
        }

        public void Equals_Clicked(object sender, RoutedEventArgs e)
        {
            if (XYClicked == true)
            {
                Result = Math.Pow(Number1, Number2);
                CalculationString = Result.ToString();
                Textbox.Text = CalculationString.Remove(CalculationString.Length - 1);
            }

            switch (RealOperator)
            {
                case "+":
                    if (OperatorCount > 1)
                    {
                        Result += Number2;

                    }
                    else
                    {
                        Result = Number1 + Number2;
                    }
                    break;

                case "-":
                    if (OperatorCount > 1)
                    {
                        Result -= Number2;

                    }
                    else
                    {
                        Result = Number1 - Number2;
                    }
                    break;

                case "÷":
                    if (OperatorCount > 1)
                    {
                        Result /= Number2;

                    }
                    else
                    {
                        Result = Number1 / Number2;
                    }
                    break;

                case "x":
                    if (OperatorCount > 1)
                    {
                        Result *= Number2;
                    }
                    else
                    {
                        Result = Number1 * Number2;
                    }
                    break;

                default:
                    this.Textbox.Text = "Invalid Input";
                    break;
            }

            this.LabelTextBox.Text = "";
            this.Textbox.Text = Result.ToString();
            Number2 = 0;
            NextInput = string.Empty;
        }        

        private void MemoryClear_Click(object sender, RoutedEventArgs e)
        {
            MemoryArray.Clear();
            MemoryCount = 0;
            Input = string.Empty;
            MC.IsEnabled = false;
            MT.IsEnabled = false;
            MR.IsEnabled = false;
        }

        private void MemoryRecall_Click(object sender, RoutedEventArgs e)
        {
            this.Textbox.Text = MemoryRecall.ToString();
        }

        private void AddWithNumberInMemory_Click(object sender, RoutedEventArgs e)
        {
            MC.IsEnabled = true;
            MT.IsEnabled = true;
            MR.IsEnabled = true;
            HistoryNumber = Convert.ToDouble(this.Textbox.Text);
            MemoryCount++;

            if (MemoryCount == 1)
            {
                MemoryArray.Add(HistoryNumber);
            }

            MemoryCount = 0;
            Input = string.Empty;
        }

        private void SubtractWithNumberInMemory_Click(object sender, RoutedEventArgs e)
        {
            MC.IsEnabled = true;
            MT.IsEnabled = true;
            MR.IsEnabled = true;
            HistoryNumber = Convert.ToDouble(this.Textbox.Text);
            MemoryCount++;

            if (MemoryCount == 1)
            {
                MemoryArray.Add(HistoryNumber);
            }

            MemoryCount = 0;
            Input = string.Empty;
        }

        private void MemoryStore_Click(object sender, RoutedEventArgs e)
        {
            //Zorgen voor juiste weergave in de textbox.
            MC.IsEnabled = true;
            MT.IsEnabled = true;
            MR.IsEnabled = true;
            MemoryStoreNumber = Convert.ToDouble(this.Textbox.Text);
            Input = string.Empty;
        }

        private void MemoryHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ToRadian()
        {
            Textbox.Text = (Math.PI * double.Parse(Textbox.Text) / 180.0).ToString();
        }

        private void ToDegrees()
        {
            Textbox.Text = (double.Parse(Textbox.Text) * (180.0 / Math.PI)).ToString();
        }

        private void ToGrad()
        {
            //Unknown
        }
        
        //Formule voor Degree to Radian
        //Math.PI * Input / 180.0;

        //Formule voor Radian to Degree
        //Input * (180.0 / Math.PI);
        

        private void GRAD_Click(object sender, RoutedEventArgs e)
        {
            GRAD.Visibility = Visibility.Hidden;
            DEG.Visibility = Visibility.Visible;
        }

        private void DEG_Click(object sender, RoutedEventArgs e)
        {
            DEG.Visibility = Visibility.Hidden;
            RAD.Visibility = Visibility.Visible;
        }

        private void RAD_Click(object sender, RoutedEventArgs e)
        {
            RAD.Visibility = Visibility.Hidden;
            GRAD.Visibility = Visibility.Visible;
        }

        private void HYP_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FE_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Sin_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Sin(double.Parse(Textbox.Text))).ToString();
        }

        private void Cos_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Cos(double.Parse(Textbox.Text))).ToString();
        }

        private void Tan_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Tan(double.Parse(Textbox.Text))).ToString();            
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Log(double.Parse(Textbox.Text))).ToString();
        }

        private void Exp_Click(object sender, RoutedEventArgs e)
        {
            Textbox.Text = (Math.Exp(double.Parse(Textbox.Text))).ToString();
        }

        private void Mod_Click(object sender, RoutedEventArgs e)
        {

        }

        private void xY_Click(object sender, RoutedEventArgs e)
        {
            LabelTextBox.Text = Number1 + "^";
            Textbox.Text = Number1.ToString();
            bool XYClicked = true;
            
        }

        private void TenthPower_Click(object sender, RoutedEventArgs e)
        {
            Result = Math.Pow(10, Number1);
            CalculationString = Result.ToString();
            Textbox.Text = CalculationString.Remove(CalculationString.Length - 1);
                                   
            //PercentageCount++;

            //if (PercentageCount > 1)
            //{
            //    Result = Result * (Number2 / 100);
            //    Textbox.Text = Result.ToString();

            //    for (int i = 1; i < PercentageCount; i++)
            //    {
            //        LabelTextBox.Text = CalculationString + " " + Result.ToString();
            //    }
            //}
            //else
            //{
            //    Result = Number1 * (Number2 / 100);
            //    Textbox.Text = Result.ToString();
            //    LabelTextBox.Text = CalculationString + " " + Result.ToString();
            //}
        }

        private void SquareRoot_Click(object sender, RoutedEventArgs e)
        {
            OperatorCount++;

            if (OperatorCount > 1)
            {
                Result = Math.Sqrt(Result);
                Textbox.Text = Result.ToString();
                CalculationString = LabelTextBox.Text;

                for (int i = 1; i < OperatorCount; i++)
                {
                    LabelTextBox.Text = "✔(" + CalculationString + ")";
                }
            }
            else
            {
                Result = Math.Sqrt(Number1);
                Textbox.Text = Result.ToString();
                LabelTextBox.Text = "✔(" + Number1.ToString() + ")";
            }
        }

        private void Exponents_Click(object sender, RoutedEventArgs e)
        {
            OperatorCount++;

            if (OperatorCount > 1)
            {
                Result = Result * Result;
                Textbox.Text = Result.ToString();
                CalculationString = LabelTextBox.Text;

                for (int i = 1; i < OperatorCount; i++)
                {
                    LabelTextBox.Text = "sqr(" + CalculationString + ")";
                }
            }
            else
            {
                Result = Number1 * Number1;
                Textbox.Text = Result.ToString();
                LabelTextBox.Text = "sqr(" + Number1.ToString() + ")";
            }
        }

        private void PlusMinus_Click(object sender, RoutedEventArgs e)
        {
            if (Textbox.Text.StartsWith("-"))
            {
                Textbox.Text = Textbox.Text.Substring(1);
            }
            else if (!string.IsNullOrEmpty(Textbox.Text))
            {
                Textbox.Text = "-" + Textbox.Text;
            }
        }

        private void ExtraCalculations_Click(object sender, RoutedEventArgs e)
        {
            UpArrow.Visibility = Visibility.Hidden;
            ClickedAgain.Visibility = Visibility.Visible;

            x2.Visibility = Visibility.Visible;
            xy.Visibility = Visibility.Visible;
            sin.Visibility = Visibility.Visible;
            cos.Visibility = Visibility.Visible;
            tan.Visibility = Visibility.Visible;
            Nike.Visibility = Visibility.Visible;
            TenthPower.Visibility = Visibility.Visible;
            log.Visibility = Visibility.Visible;
            Exp.Visibility = Visibility.Visible;
            Mod.Visibility = Visibility.Visible;

            powerthree.Visibility = Visibility.Hidden;
            yrootx.Visibility = Visibility.Hidden;
            sin1.Visibility = Visibility.Hidden;
            cos1.Visibility = Visibility.Hidden;
            tan1.Visibility = Visibility.Hidden;
            dividex.Visibility = Visibility.Hidden;
            ex.Visibility = Visibility.Hidden;
            ln.Visibility = Visibility.Hidden;
            dms.Visibility = Visibility.Hidden;
            deg.Visibility = Visibility.Hidden;

        }
        private void ClickedAgain_Click(object sender, RoutedEventArgs e)
        {
            ClickedAgain.Visibility = Visibility.Hidden;
            UpArrow.Visibility = Visibility.Visible;

            x2.Visibility = Visibility.Hidden;
            xy.Visibility = Visibility.Hidden;
            sin.Visibility = Visibility.Hidden;
            cos.Visibility = Visibility.Hidden;
            tan.Visibility = Visibility.Hidden;
            Nike.Visibility = Visibility.Hidden;
            TenthPower.Visibility = Visibility.Hidden;
            log.Visibility = Visibility.Hidden;
            Exp.Visibility = Visibility.Hidden;
            Mod.Visibility = Visibility.Hidden;

            powerthree.Visibility = Visibility.Visible;
            yrootx.Visibility = Visibility.Visible;
            sin1.Visibility = Visibility.Visible;
            cos1.Visibility = Visibility.Visible;
            tan1.Visibility = Visibility.Visible;
            dividex.Visibility = Visibility.Visible;
            ex.Visibility = Visibility.Visible;
            ln.Visibility = Visibility.Visible;
            dms.Visibility = Visibility.Visible;
            deg.Visibility = Visibility.Visible;
        }

        private void Window_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
    }
}