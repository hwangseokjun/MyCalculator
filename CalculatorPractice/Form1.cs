using System;
using System.Windows.Forms;

namespace CalculatorPractice
{
    delegate void Operator();
    public partial class Form1 : Form
    {

        // TODO: 계산 기록 로그 남기기 적용 필요

        private decimal? numA = null;
        private decimal? numB = null;
        private decimal? result = null;
        private Operator _operator = null;

        private Button numBtn = null;
        private Button oprtBtn = null;

        private bool isClickOperator = false;
        private bool isClickEqual = false;
        private bool isCalculated = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void NumClick(object sender, EventArgs e)
        {
            this.numBtn = (Button)sender;

            InitializeInputbox();
            WriteNumber();
        }

        private void OperatorClick(object sender, EventArgs e)
        {
            this.oprtBtn = (Button)sender;
            this.isClickOperator = true;

            AssignOperator();
            AssignNumbers();
        }

        private void EraseClick(object sender, EventArgs e) 
        {
            // 숫자 입력을 시작한 경우 삭제, 그 이외에는 처리하지 않음
        }

        private void DotClick(object sender, EventArgs e) 
        {
            InitializeInputbox();
            WriteDot();
        }

        private void ClearClick(object sender, EventArgs e) 
        {
            ClearAll();
        }

        private void ClearAll() 
        {
            this.numA = null;
            this.numB = null;
            this.result = null;
            this._operator = null;
            this.textBox1.Text = "0";
        }

        private void EqualClick(object sender, EventArgs e) 
        {
            this.isClickOperator = true;

            AssignNumbers();
            CalculateResult();
            WriteResult();
            Flush();
        }

        private void InitializeInputbox()
        {
            if (isClickOperator)
            {
                this.textBox1.Text = "0";
                this.isClickOperator = false;
            }
        }

        private void WriteNumber()
        {
            if (this.textBox1.Text == "0")
                this.textBox1.Text = this.numBtn.Text;
            else
                this.textBox1.Text += this.numBtn.Text;
        }

        private void WriteDot() 
        {
            if (!this.textBox1.Text.Contains("."))
                this.textBox1.Text += ".";
        }


        private void AssignOperator() 
        {
            this._operator = this.oprtBtn.Text switch
            {
                "/" => new Operator(Divide),
                "*" => new Operator(Multiply),
                "-" => new Operator(Substract),
                "+" => new Operator(Add),
                _ => null
            };
        }

        private void AssignNumbers() 
        {
            if (this.numA == null)
                this.numA = decimal.Parse(this.textBox1.Text);
            else
                this.numB = decimal.Parse(this.textBox1.Text);
        }

        private void CalculateResult() 
        {
            if (this._operator == null)
                return;
            
            if (
                this.numA != null && 
                this.numB != null &&
                this._operator != null) 
            {
                this._operator();
                this.isCalculated = true;
            }
        }

        private void WriteResult() 
        {
            this.textBox1.Text = this.result.ToString();
        }

        private void Flush() 
        {
            this.result = null;
            this.numA = null;
        }

        private void Add() 
        {
            this.result = this.numA + this.numB;
        }

        private void Substract() 
        {
            this.result = this.numA - this.numB;
        }

        private void Multiply() 
        {
            this.result = this.numA * this.numB;
        }

        private void Divide() 
        {
            this.result = this.numA / this.numB;
        }

    }
}
