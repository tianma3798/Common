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
using System.Drawing;

namespace ColorConvert
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Bind(System.Drawing.Color.Red);

            txtA.TextChanged += ARGB_ValueChange;
            txtR.TextChanged += ARGB_ValueChange;
            txtG.TextChanged += ARGB_ValueChange;
            txtB.TextChanged += ARGB_ValueChange;

            //RGB 输入框
            txtRGB.TextChanged += RGB_TextChange;
            txtRGB2.TextChanged += RGB_TextChange;

            //ARGB 输入框
            txtARGB.TextChanged += ARGB_TextChanged;
            txtARGB2.TextChanged += ARGB_TextChanged;

            //16进制
            txtOne.TextChanged += txtOne_TextChanged;
            txtOne2.TextChanged += txtOne_TextChanged;

            //绑定输入框的双击事件
            foreach (var item in gridOne.Children)
            {
                TextBox txt = item as TextBox;
                if (txt != null)
                {
                    txt.MouseDoubleClick +=delegate{
                        txt.SelectAll();
                    };
                }
            }
        }



        /// <summary>
        /// 16进制输入框
        /// </summary>
        private void txtOne_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                //1.获取16进制
                TextBox txt = sender as TextBox;
                if (txt == null)
                    return;
                string value = txt.Text;
                value = value.Replace("#", "");
                if (value.Length > 6)
                    value = value.Substring(0,6);
                value = "#" + value;

                if (value.Length == 7||value.Length==4)
                {
                    System.Drawing.Color color = System.Drawing.ColorTranslator.FromHtml(value);
                    //绑定显示
                    Bind(color);
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// RGB输入框
        /// </summary>
        private void RGB_TextChange(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                if (txt == null)
                    return;
                //rgb(255,0,0) ,255,0,0
                string value = txt.Text;
                int R;
                int G;
                int B;
                if (value.Contains("(") && value.Contains(")"))
                {
                    int start = value.IndexOf("(") + 1;
                    int length = value.IndexOf(")") - start;
                    value = value.Substring(start, length);

                    int[] result = value.Split(',').Select(q => Convert.ToInt32(q)).ToArray();
                    R = result[0];
                    G = result[1];
                    B = result[2];
                }
                else
                {
                    int[] result = value.Split(',').Select(q => Convert.ToInt32(q)).ToArray();
                    R = result[0];
                    G = result[1];
                    B = result[2];
                }
                //最大值过滤
                R = R > 255 ? 255 : R;
                G = G > 255 ? 255 : G;
                B = B > 255 ? 255 : B;

                System.Drawing.Color color = System.Drawing.Color.FromArgb(255, R, G, B);
                //绑定显示
                Bind(color);
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// ARGB输入框
        /// </summary>
        private void ARGB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox txt = sender as TextBox;
                if (txt == null)
                    return;
                //rgb(255,0,0) ,255,0,0
                string value = txt.Text;
                int A;
                int R;
                int G;
                int B;
                if (value.Contains("(") && value.Contains(")"))
                {
                    int start = value.IndexOf("(") + 1;
                    int length = value.IndexOf(")") - start;
                    value = value.Substring(start, length);

                    int[] result = value.Split(',').Select(q => Convert.ToInt32(q)).ToArray();
                    A = result[0];
                    R = result[1];
                    G = result[2];
                    B = result[3];
                }
                else
                {
                    int[] result = value.Split(',').Select(q => Convert.ToInt32(q)).ToArray();
                    A = result[0];
                    R = result[1];
                    G = result[2];
                    B = result[3];
                }
                //最大值过滤
                A = A > 255 ? 255 : A;
                R = R > 255 ? 255 : R;
                G = G > 255 ? 255 : G;
                B = B > 255 ? 255 : B;

                System.Drawing.Color color = System.Drawing.Color.FromArgb(255, R, G, B);
                //绑定显示
                Bind(color);
            }
            catch (Exception ex)
            {
            }
        }
        //RGBA颜色值改变
        private void ARGB_ValueChange(object sender, TextChangedEventArgs e)
        {
            try
            {

                //判断当前输入框输入内容位数和个数
                TextBox current = sender as TextBox;
                if (current == null)
                    return;
                if (current.Text.Length > 3)
                {
                    current.Text = current.Text.Substring(0, 3);
                    return;
                }
                int number = int.Parse(current.Text);
                if (number > 255)
                    current.Text = "255";

                int A = int.Parse(txtA.Text);
                int R = int.Parse(txtR.Text);
                int G = int.Parse(txtG.Text);
                int B = int.Parse(txtB.Text);


                System.Drawing.Color color = System.Drawing.Color.FromArgb(A, R, G, B);
                //绑定显示
                Bind(color);
            }
            catch (Exception)
            {
                return;
            }
        }
        /// <summary>
        /// 指定颜色绑定显示
        /// </summary>
        /// <param name="color"></param>
        private void Bind(System.Drawing.Color color)
        {
            //1.16进制
            txtOne.Text= ColorTranslator.ToHtml(System.Drawing.Color.FromArgb(color.A,
                color.R,
                color.G,
                color.B)).ToUpper();
            txtOne2.Text = txtOne.Text.Replace("#","");

            //3.ARGB
            txtA.Text = color.A.ToString();
            txtR.Text = color.R.ToString();
            txtG.Text = color.G.ToString();
            txtB.Text = color.B.ToString();

            txtARGB2.Text = string.Format("{0},{1},{2},{3}", color.A,
                color.R,
                color.G,
                color.B);
            txtARGB.Text = string.Format("argb({0})", txtARGB2.Text);
            //2.RGB
            txtRGB2.Text = string.Format("{0},{1},{2}",
                color.R,
                color.G,
                color.B);
            txtRGB.Text = string.Format("rgb({0})", txtRGB2.Text);
       


            //4.显示按钮
            colorTarget.Background =new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A,color.R,color.G,color.B));
        }
    }
}
