using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Web;

namespace System.Drawing
{
    /// <summary>
    /// GDI+ 常用方法
    /// </summary>
    public class ImgHelper
    {

        #region 组合处理图片
        /// <summary>
        /// 固定比例，缩放，剪切图片
        /// </summary>
        /// <param name="img">位图</param>
        /// <param name="targetWidth">最大宽度</param>
        /// <param name="targetHeight">最大高度</param>
        /// <returns></returns>
        public static Bitmap ResizeCut(Bitmap img, int targetWidth, int targetHeight)
        {
            Graphics g = Graphics.FromImage(img);
            /*
             * 适用大小 targetWidth*targetHeight
             * 宽度最大 targetWidth，多余剪切
             * 高度最大 targetHeight，多余剪切
             */
            double scaleTarget = targetWidth * 1.0 / targetHeight;
            double scaleImg = img.Width * 1.0 / img.Height;
            if (scaleImg >= scaleTarget)
            {
                //以高为标准缩放，后剪切
                float scaleHeight = Convert.ToSingle(targetHeight * 1.0 / img.Height);
                int newWidth = Convert.ToInt32(img.Width * scaleHeight);
                //缩放
                img = ImgHelper.ResizeImage(img, newWidth, targetHeight);
                //剪切
                int resultWidth = newWidth > targetWidth ? targetWidth : newWidth;
                img = ImgHelper.Cut(img, (newWidth - targetWidth) / 2, 0, resultWidth, targetHeight);
            }
            else
            {
                //以宽为标准，先缩放，后剪切
                int newHeight = Convert.ToInt32(img.Height * (targetWidth * 1.0 / img.Width));
                //缩放
                img = ImgHelper.ResizeImage(img, targetWidth, newHeight);
                int resultHeight = newHeight > targetHeight ? targetHeight : newHeight;
                //剪切
                img = ImgHelper.Cut(img, 0, (newHeight - targetHeight) / 2, targetWidth, resultHeight);
            }
            return img;
        }
        /// <summary>
        /// 固定比例，缩放，剪切图片
        /// </summary>
        /// <param name="fullname">图片位置</param>
        /// <param name="targetWidth">最大宽度</param>
        /// <param name="targetHeight">最大高度</param>
        /// <returns></returns>
        public static Bitmap ResizeCut(string fullname, int targetWidth, int targetHeight)
        {
            Bitmap img = GetBitmapForGra(fullname);
            return ResizeCut(img,targetWidth,targetHeight);
        }
        #endregion


        /// <summary>
        /// 判断指定PixelFormat  是否带有索引
        /// </summary>
        /// <param name="format">PixelFormat枚举</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(PixelFormat format)
        {
            PixelFormat[] indexedPixelFormats = {
                        PixelFormat.Undefined,
                        PixelFormat.DontCare,
                        PixelFormat.Format16bppArgb1555,
                        PixelFormat.Format1bppIndexed,
                        PixelFormat.Format4bppIndexed,
                        PixelFormat.Format8bppIndexed
                            };
            foreach (PixelFormat item in indexedPixelFormats)
            {
                if (item.Equals(format))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 为获取 Graphics 对象 先过滤 带索引情况下
        /// </summary>
        /// <param name="bmp">Bitmap对象</param>
        /// <returns></returns>
        public static Bitmap GetBitmapForGra(Bitmap bmp)
        {
            if (IsPixelFormatIndexed(bmp.PixelFormat))
            {
                //需要转换
                Bitmap bit = new Bitmap(bmp.Width, bmp.Height, PixelFormat.Format32bppArgb);

                Graphics g = Graphics.FromImage(bit);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.DrawImage(bmp, 0, 0);
                g.Dispose();

                return bit;
            }
            return bmp;
        }
        /// <summary>
        /// 为获取 Graphics 对象 先过滤 带索引情况下
        /// </summary>
        /// <param name="filename">Bitmap对象</param>
        /// <returns></returns>
        public static Bitmap GetBitmapForGra(string filename)
        {
            Bitmap bit = new Bitmap(filename);
            return GetBitmapForGra(bit);
        }
        /// <summary>  
        ///  Resize图片
        /// </summary>  
        /// <param name="bmp">原始Bitmap </param>  
        /// <param name="newW">新的宽度</param>  
        /// <param name="newH">新的高度</param>  
        /// <returns>处理以后的图片</returns>  
        public static Bitmap ResizeImage(Bitmap bmp, int newW, int newH)
        {
            try
            {
                Bitmap b = new Bitmap(newW, newH);
                Graphics g = Graphics.FromImage(b);
                // 插值算法的质量   
                //g.InterpolationMode = InterpolationMode.NearestNeighbor;
                g.DrawImage(bmp, new Rectangle(0, 0, newW, newH), new Rectangle(0, 0, bmp.Width, bmp.Height), GraphicsUnit.Pixel);
                g.Dispose();
                return b;
            }
            catch
            {
                return null;
            }
        }
        /// <summary>  
        /// 剪裁 -- 用GDI+   
        /// </summary>  
        /// <param name="b">原始Bitmap</param>  
        /// <param name="StartX">开始坐标X</param>  
        /// <param name="StartY">开始坐标Y</param>  
        /// <param name="iWidth">宽度</param>  
        /// <param name="iHeight">高度</param>  
        /// <returns>剪裁后的Bitmap</returns>  
        public static Bitmap Cut(Bitmap b, int StartX, int StartY, int iWidth, int iHeight)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            if (StartX >= w || StartY >= h)
            {
                return null;
            }
            if (StartX + iWidth > w)
            {
                iWidth = w - StartX;
            }
            if (StartY + iHeight > h)
            {
                iHeight = h - StartY;
            }
            try
            {
                Bitmap bmpOut = new Bitmap(iWidth, iHeight, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(b, new Rectangle(0, 0, iWidth, iHeight), new Rectangle(StartX, StartY, iWidth, iHeight), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }


        #region 生成验证码
/// <summary>
/// 生成内存位图
/// </summary>
/// <param name="Code"></param>
/// <returns></returns>
public static Bitmap GetCode(out string Code)
{
    int imgWidth = 80;
    int imgHeight = 40;
    //获取随机字符
    Code = DateTimeHelper.GetCode_Ran(4);
    //颜色列表，用于验证码、噪线、噪点 
    Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };
    //字体列表，用于验证码 
    string[] font = { "Times New Roman", "MS Mincho", "Book Antiqua", "Gungsuh", "PMingLiU", "Impact" };

    Bitmap bmp = new Bitmap(imgWidth, imgHeight);
    Graphics g = Graphics.FromImage(bmp);
    g.Clear(Color.White);
    Random rnd = new Random();
    //画噪线 
    for (int i = 0; i < 10; i++)
    {
        int x1 = rnd.Next(imgWidth);
        int y1 = rnd.Next(imgHeight);
        int x2 = rnd.Next(imgWidth);
        int y2 = rnd.Next(imgHeight);
        Color clr = color[rnd.Next(color.Length)];
        g.DrawLine(new Pen(clr), x1, y1, x2, y2);
    }
    //画验证码字符串 
    for (int i = 0; i < Code.Length; i++)
    {
        string fnt = font[rnd.Next(font.Length)];
        Font ft = new Font(fnt, 18);
        Color clr = color[rnd.Next(color.Length)];
        g.DrawString(Code[i].ToString(), ft, new SolidBrush(clr), (float)i * 19, (float)8);
    }
    //画噪点 
    for (int i = 0; i < 100; i++)
    {
        int x = rnd.Next(bmp.Width);
        int y = rnd.Next(bmp.Height);
        Color clr = color[rnd.Next(color.Length)];
        bmp.SetPixel(x, y, clr);
    }
    //显式释放资源 
    // bmp.Dispose();
    g.Dispose();
    return bmp;
}
/// <summary>
/// 生成位图，输出到响应流
/// </summary>
/// <param name="Response"></param>
/// <param name="Code"></param>
public static void GetCode(HttpResponseBase Response, out string Code)
{
    Code = string.Empty;
    Bitmap bit = GetCode(out Code);

    ////清除该页输出缓存，设置该页无缓存 
    //Response.Buffer = true;
    //Response.ExpiresAbsolute = DateTime.Now.AddMilliseconds(0);
    //Response.Expires = 0;
    //Response.CacheControl = "no-cache";
    //Response.AppendHeader("Pragma", "No-Cache");

    Response.ClearContent();
    bit.Save(Response.OutputStream, ImageFormat.Png);
    Response.ContentType = "image/png";

    //释放资源
    bit.Dispose();
}
        #endregion

    }
}
