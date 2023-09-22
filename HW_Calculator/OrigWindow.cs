using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_Calculator
{
    internal class OrigWindow
    {
        public static Region myRegion;
        static public void setOriginalForm(Form form, string _form = "normal")
        {
            // myRegion = new Region();
            // Создаём форму границ - объект GraphicsPath
            System.Drawing.Drawing2D.GraphicsPath myPath = new System.Drawing.Drawing2D.GraphicsPath();
            if (_form == "octagon") //восьмиграник
            {
                form.FormBorderStyle = FormBorderStyle.None;
                form.BackColor = Color.BurlyWood;

                myPath.AddPolygon(new Point[] { new Point(form.Width/4, 0),
                                                new Point(form.Width-form.Width/4, 0),
                                                new Point(form.Width, form.Height/4),
                                                new Point(form.Width, form.Height-form.Height/4),
                                                new Point(form.Width-form.Width/4, form.Height),
                                                new Point(form.Width/4, form.Height),
                                                new Point(0, form.Height-form.Height/4),
                                                new Point(0, form.Height/4) });
                myRegion = new Region(myPath);
                form.Region = myRegion;
            }
            else
                if (_form == "ellipse") //элипс
            {
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.BackColor = Color.DimGray;
                    myPath.AddEllipse(0, 0, form.Width, form.Height);
                    myRegion = new Region(myPath);
                    form.Region = myRegion;
                }
            else
                if (_form == "trapezoid") //трапеция
            {
                    form.FormBorderStyle = FormBorderStyle.None;
                    Point[] myPoint = { new Point(0,0),
                    new Point(form.Width, 0),
                    new Point(form.Width-50, form.Height),
                    new Point(50, form.Height)};
                    myPath.AddPolygon(myPoint);
                    myRegion = new Region(myPath);
                    form.Region = myRegion;
                    form.BackColor = Color.DarkCyan;
                }
            else
            {
                form.FormBorderStyle = FormBorderStyle.FixedSingle;
                form.BackColor = Form.DefaultBackColor;
            }        
        }
        static public void setOriginalButton(Button btn, string _form = "normal")
        {
            System.Drawing.Drawing2D.GraphicsPath myPathButt = new System.Drawing.Drawing2D.GraphicsPath();
            if (_form == "octagon")
            {                
                btn.BackColor = Color.Chocolate;
                myPathButt.AddPolygon(new Point[] { new Point(btn.Size.Width/3, 0),
                                                new Point(btn.Size.Width-btn.Size.Width/3, 0),
                                                new Point(btn.Size.Width, btn.Size.Height/3),
                                                new Point(btn.Size.Width, btn.Size.Height-btn.Size.Height/3),
                                                new Point(btn.Size.Width-btn.Size.Width/3, btn.Size.Height),
                                                new Point(btn.Size.Width/3, btn.Size.Height),
                                                new Point(0, btn.Size.Height-btn.Size.Height/3),
                                                new Point(0, btn.Size.Height/3) });
                myRegion = new Region(myPathButt);
                btn.Region = myRegion;
            }
            else
                if (_form == "ellipse")
            {
                btn.BackColor = Color.BlueViolet;
                myPathButt.AddEllipse(3, 3, btn.Width - 7, btn.Height - 7);//3 и 7 магические числа, для корреции ввида                                               
                myRegion = new Region(myPathButt);
                btn.Region = myRegion;

            }
            else
                if (_form == "trapezoid")
            {
                Point[] myPoint = { new Point(3,5),
                                    new Point(btn.Width-5, 5),
                                    new Point(btn.Width-10, btn.Height-5),
                                    new Point(8, btn.Height-5)};// 3,5,8,10 - магические числа для визуализации
                myPathButt.AddPolygon(myPoint);
                myRegion = new Region(myPathButt);
                btn.Region = myRegion;
                btn.BackColor = Color.DarkKhaki;
            }
            else
            {
                btn.BackColor = Button.DefaultBackColor;                
                myPathButt.AddPolygon(new Point[] { new Point(0, 0),
                                                new Point(btn.Size.Width, 0),
                                                new Point(btn.Size.Width, btn.Size.Height),
                                                new Point(0, btn.Size.Height) });
                myRegion = new Region(myPathButt);
                btn.Region = myRegion;
            }

        }
    }
}
