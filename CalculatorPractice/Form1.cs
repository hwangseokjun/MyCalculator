using System;
using System.Windows.Forms;

namespace CalculatorPractice
{
    delegate void Operator();
    public partial class Form1 : Form
    {
        private decimal? numA;
        private decimal? numB;
        private Operator _operator;
        private Button button;
        private bool isClickOperator = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void NumClick(object sender, EventArgs e)
        {
            this.button = (Button)sender;

            InitializeTextboxIfNeeded();
            WriteNumber();
        }

        private void OperatorClick(object sender, EventArgs e)
        {
            this.button = (Button)sender;
            this.isClickOperator = true;

            AssignOperator(); // 오퍼레이터 할당
            AssignNumbers(); // 숫자 할당
        }

        private void EraseClick(object sender, EventArgs e) 
        {
            // 숫자 입력을 시작한 경우 삭제, 그 이외에는 처리하지 않음
        }

        private void DotClick(object sender, EventArgs e) 
        {
            
        }

        private void ClearClick(object sender, EventArgs e) 
        {
            this.numA = null;
            this.numB = null;
            this._operator = null;
            this.textBox1.Text = "0";
        }

        private void EqualClick(object sender, EventArgs e) 
        {
            this.isClickOperator = true;

            AssignNumbers();
            CalculateResultIfNeeded();
        }

        private void InitializeTextboxIfNeeded()
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
                this.textBox1.Text = this.button.Text;
            else
                this.textBox1.Text += this.button.Text;
        }


        private void AssignOperator() 
        {
            this._operator = this.button.Text switch
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

        private void CalculateResultIfNeeded() 
        {
            if (this._operator == null)
                return;
            
            if (this.numA != null && this.numB != null) 
            {
                this._operator();
                this.textBox1.Text = this.numA.ToString();
                this.numA = null;
            }
        }

        private void Add() 
        {
            this.numA += this.numB;
        }

        private void Substract() 
        {
            this.numA -= this.numB;
        }

        private void Multiply() 
        {
            this.numA *= this.numB;
        }

        private void Divide() 
        {
            this.numA /= this.numB;
        }
    }
}
