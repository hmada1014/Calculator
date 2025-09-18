using Calculator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static AngouriMath.Entity;
using Button = System.Windows.Forms.Button;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        enum enOperation
        {
            Add = 1,
            Sup = 2,
            Multi = 3,
            Divide = 4
        }

        enOperation CurrentOpration;

        bool newOpration = false;

        bool IsOpration(char Text)
        {
            if (Text == '+')
            {
                return false;
            }
            else if (Text == '-')
            {
                return false;
            }
            else if (Text == 'X')
            {
                return false;
            }
            else if (Text == '÷')
            {
                return false;
            }
            else if (Text == '.')
            {
               
                return false;
            }
            else
            {
                return true;
            }
        }

        
        void RemoveLastElement()
        {

          
            txtDisplayScreen.Text = txtDisplayScreen.Text.Remove(txtDisplayScreen.Text.Length - 1, 1);
            if (txtDisplayScreen.Text == "")
            {
                Clear();
            }
            if(txtDisplayScreen.Text != "")
            {
                EnabeledAllButton(txtDisplayScreen.Text, true);
            }
        }

        void Clear()
        {
            txtDisplayScreen.Text = "0";
        }

        void SetNumber(Button CurrentBtn)
        {
            if (txtDisplayScreen.Text.ToString() == "0" || newOpration)
            {
                txtDisplayScreen.Text = CurrentBtn.Tag.ToString();
                newOpration = false;
            }
            else
            {
                txtDisplayScreen.Text += CurrentBtn.Tag.ToString();
                if (CurrentOpration.ToString() != "0")
                {
                    EnabeledAllButton(CurrentOpration.ToString());
                   
                }
            }
        }

        float AddOperation(string[] Num, float Sum)
        {
            Num = txtDisplayScreen.Text.Split('+');
            Sum = 0;
            for (int i = 0; i < Num.Length; i++)
            {
                Sum += Convert.ToSingle(Num[i]);
            }
            return Sum;
        }

        float SupOperation(string[] Num, float Sum)
        {
            if (txtDisplayScreen.Text[0] != '-')
            {

                Num = txtDisplayScreen.Text.Split('-');
                Sum = Convert.ToSingle(Num[0]);
                for (int i = 1; i < Num.Length; i++)
                {
                    Sum -= Convert.ToSingle(Num[i]);
                }
            }
            else
            {
                Num = txtDisplayScreen.Text.Split('-');
                string Num1 = txtDisplayScreen.Text[0] + Num[1];
                Sum = Convert.ToSingle(Num1);
                for (int i = 2; i < Num.Length; i++)
                {
                    Sum -= Convert.ToSingle(Num[i]);
                }
            }

            return Sum;
        }

        float DividOperation(string[] Num, float Sum)
        {
            Num = txtDisplayScreen.Text.Split('÷');
            Sum = Convert.ToSingle(Num[0]);
            for (int i = 1; i < Num.Length; i++)
            {

                Sum /= Convert.ToSingle(Num[i]);

            }
            return Sum;
        }

        float Multiperation(string[] Num, float Sum)
        {
            Num = txtDisplayScreen.Text.Split('X');
            Sum = Convert.ToSingle(Num[0]);
            for (int i = 1; i < Num.Length; i++)
            {
                Sum *= Convert.ToSingle(Num[i]);
            }
            return Sum;
        }

        void CalclatorOperation(enOperation Opration)
        {
            string[] Num = null;
            float Sum = 0;
            switch (Opration)
            {
                case enOperation.Add:

                    Sum = AddOperation(Num, Sum);
                    txtDisplayScreen.Text = Convert.ToString(Sum);
                    break;

                case enOperation.Sup:

                    Sum = SupOperation(Num, Sum);
                    txtDisplayScreen.Text = Convert.ToString(Sum);


                    break;

                case enOperation.Divide:

                    Sum = DividOperation(Num, Sum);
                    txtDisplayScreen.Text = Convert.ToString(Sum);

                    break;

                case enOperation.Multi:

                    Sum = Multiperation(Num, Sum);
                    txtDisplayScreen.Text = Convert.ToString(Sum);

                    break;


            }
        }

        void EnabeledAllButton(string Text, bool AllTrue = false)
        {
          btnDivide.Enabled   = (((Text == btnDivide.Text)   || (Text == "Divide")) || AllTrue ? true : false);
          btnTims.Enabled     = (((Text == btnTims.Text)     || (Text == "Multi")) || AllTrue ? true : false);
          btnSubtract.Enabled = (((Text == btnSubtract.Text) || (Text == "Sup")) || AllTrue ? true : false);
          btnAdd.Enabled      = (((Text == btnAdd.Text)      || (Text == "Add")) || AllTrue ? true : false); 

        }

        bool IsLastIsNumber(string Text)
        {
            int Last = Text.Length - 1;

            return IsOpration(Text[Last]);
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (IsLastIsNumber(txtDisplayScreen.Text))
            {
                CalclatorOperation(CurrentOpration);
                CurrentOpration = 0;
                newOpration = true;
                EnabeledAllButton("", true);
            }
            else
            {
                if (MessageBox.Show($"Opration Is Wrong :{Environment.NewLine}Do You want to continuo the Opration ", "Wrong",
                MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    
                    EnabeledAllButton(CurrentOpration.ToString());

                }
                else
                {
                    txtDisplayScreen.Text = "0";
                    EnabeledAllButton(Text, true);
                }
            }


            

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            EnabeledAllButton("", true);
            CurrentOpration = 0;
            Clear();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            RemoveLastElement();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            SetNumber((Button)sender);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsLastIsNumber(txtDisplayScreen.Text))
            {
                txtDisplayScreen.Text += "+";
            }
            newOpration = false;

            EnabeledAllButton("+");


            CurrentOpration = enOperation.Add;
        }

        private void btnSubtract_Click(object sender, EventArgs e)
        {
            if (IsLastIsNumber(txtDisplayScreen.Text))
            {
                txtDisplayScreen.Text += "-";
            }
            newOpration = false;


            EnabeledAllButton("-");


            CurrentOpration = enOperation.Sup;
        }

        private void btnDivide_Click(object sender, EventArgs e)
        {
            if (IsLastIsNumber(txtDisplayScreen.Text))
            {
                txtDisplayScreen.Text += "÷";
            }
            newOpration = false;

            EnabeledAllButton("÷");
            CurrentOpration = enOperation.Divide;
        }

        private void btnTims_Click(object sender, EventArgs e)
        {
            if (IsLastIsNumber(txtDisplayScreen.Text))
            {
                txtDisplayScreen.Text += "X";
            }
            newOpration = false;

            EnabeledAllButton("X");
            CurrentOpration = enOperation.Multi; 
            
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (IsLastIsNumber(txtDisplayScreen.Text))
            {
                txtDisplayScreen.Text += ".";
            }
        }
    }
}
