using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_Calculator
{
    public partial class Form_calculator : Form
    {
        List<Button> buttons;
        Point pointStart;
        //double num1, num2;
        //string input = string.Empty;
        //char symbol = ' ';
        //Form_calculator myCalc;
        static Size _window = Screen.PrimaryScreen.Bounds.Size;
        int _heigthForm = _window.Height / 3;
        int _widthForm = _window.Width / 4;
        public Form_calculator()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text files(*.txt)|*.txt|All files(*.*)|*.*";
            //addChoiceForm();
            buttons = new List<Button>() { btn_clear, btn_erase, btn_exponentiation, btn_percent, btn_multiply, btn_divide, btn_minus, btn_plus, btn_equals, btn_comma, btn_plus_minus, btn_0, btn_1, btn_2, btn_3, btn_4, btn_5, btn_6, btn_7, btn_8, btn_9, but_save_res };
            this.MouseDown += Form1_MouseDown;
            this.MouseMove += Form1_MouseMove;
        }
        private double first_num;
        private double second_num; 
        private char user_action = ' ';
        private double res;
        private bool is_equal = false;
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            // если нажата левая кнопка мыши
            if (e.Button == MouseButtons.Left)
            {
                pointStart = new Point(e.X, e.Y);
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            // если нажата левая кнопка мыши
            if ((e.Button & MouseButtons.Left) != 0)
            {
                // получаем новую точку положения формы
                Point deltaPos = new Point(e.X - pointStart.X, e.Y - pointStart.Y);
                // устанавливаем положение формы
                this.Location = new Point(this.Location.X + deltaPos.X,
                  this.Location.Y + deltaPos.Y);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            addChoiceForm();
        }
        private void addChoiceForm()
        {            
            var items = new object[] { "normal", "octagon", "ellipse", "trapezoid" };
            cb_viewForm.Items.AddRange(items);
            cb_viewForm.SelectedValueChanged += CmbChoice_SelectedValueChanged;
            this.Controls.Add(cb_viewForm);
        }      

        private void CmbChoice_SelectedValueChanged(object sender, EventArgs e)
        {
            OrigWindow.setOriginalForm(this, ((ComboBox)sender).SelectedItem.ToString());
            foreach (var button in buttons)
            {
                OrigWindow.setOriginalButton(button, ((ComboBox)sender).SelectedItem.ToString());
            }
        }

       











        //Далее функции калькулятора
        private void BtnNumb_Click(object sender, EventArgs e)
        {
            label_input.ForeColor = Color.White;
            Button button = (Button)sender;
            if (label_input.Text == "0" || is_equal == true)
            {
                label_input.Text = button.Text;                
                is_equal = false;
            }
            else
            {
                label_input.Text += button.Text;              
            }
        }
        private void mathematical_action(char action)
        {
            first_num = Convert.ToDouble(label_input.Text);
            user_action = action;
            label_input.Text = "0"; 
            label_output.Text = "";
        }
        private void btn_equals_Click(object sender, EventArgs e)
        {
            if (user_action == ' ') 
            {
                return;
            }
            second_num = Convert.ToDouble(label_input.Text);            
            switch (user_action)
            {
                case '+': 
                    res = first_num + second_num;                    
                    break;
                case '-':
                    res = first_num - second_num;
                    break;
                case '*':
                    res = first_num * second_num;
                    break;
                case '/':
                    if (second_num == 0)
                    {
                       res = 0;                       
                       label_input.ForeColor = Color.Red;
                       label_output.Text += "Действие не возможно! ";
                       label_output.ForeColor = Color.Red;
                        MessageBox.Show("На ноль делить нельзя", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                        res = first_num / second_num;
                    }                    
                    break;
                case '%':
                    res = first_num * second_num / 100;
                    break;
                case '^':
                    res = Math.Pow(first_num, second_num);
                    break;                    
            }
            is_equal = true;
            label_input.Text = res.ToString();            
            label_output.Text += first_num.ToString() + " " + user_action + " " + second_num.ToString() + " = " + res.ToString() + "\n";
        }
        private void btn_divide_Click(object sender, EventArgs e)
        {
            mathematical_action('/');
        }

        private void btn_plus_Click(object sender, EventArgs e)
        {
            mathematical_action('+');
        }

        private void btn_minus_Click(object sender, EventArgs e)
        {
            mathematical_action('-');
        }

        private void btn_multiply_Click(object sender, EventArgs e)
        {
            mathematical_action('*');
        }

        private void btn_percent_Click(object sender, EventArgs e)
        {
            mathematical_action('%');
        }

        private void btn_exponentiation_Click(object sender, EventArgs e)
        {
            mathematical_action('^');
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            label_input.Text = "0";
            label_output.Text = "";
            label_input.ForeColor = Color.White;
            label_output.ForeColor = Color.White;
            first_num = 0;
            second_num = 0;
            user_action = ' ';
            is_equal = false;
        }

        private void btn_plus_minus_Click(object sender, EventArgs e)
        {
            double num = Convert.ToDouble(label_input.Text);
            num *= -1;
            label_input.Text = num.ToString();            
        }

        private void btn_comma_Click(object sender, EventArgs e)
        {
            if (!label_input.Text.Contains(","))
            {
                label_input.Text += ",";                
            }            
        }

        private void btn_erase_Click(object sender, EventArgs e)
        {
           if (label_input.Text.Length > 1)
           {
               label_input.Text = label_input.Text.Remove(label_input.Text.Length - 1);              
            }
           else
           {
               return;
           }    
        }

        private void but_save_res_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            System.IO.File.WriteAllText(filename, label_output.Text);
            MessageBox.Show($" Результат сохранен в файл:\n{filename}\n");
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
