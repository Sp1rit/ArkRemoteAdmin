using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkRemoteAdmin.UI.Controls
{
    internal static class GraphicUtils
    {
        public static GraphicsPath RoundRect(int x, int y, int width, int height, int radius, int lw)
        {
            GraphicsPath graphicsPath = new GraphicsPath();
            int num = radius * 2;
            graphicsPath.AddArc(x + lw, y, num, num, 180f, 90f);
            graphicsPath.AddArc(x + (width - num - lw), y, num, num, 270f, 90f);
            graphicsPath.AddArc(x + (width - num - lw), y + (height - num - lw), num, num, 360f, 90f);
            graphicsPath.AddArc(x + lw, y + (height - num - lw), num, num, 90f, 90f);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        public static void paintButtonBorder(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(SystemBrushes.Control, e.ClipRectangle);
            e.Graphics.DrawLine(SystemPens.ControlLight, new Point(0, 0), new Point(e.ClipRectangle.Width, 0));
        }

        public static void paintBottomLine(PaintEventArgs e)
        {
            e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, e.ClipRectangle.Height - 1), new Point(e.ClipRectangle.Width, e.ClipRectangle.Height - 1));
        }

        public static void drawInsertedRectangle(PaintEventArgs e)
        {
            Rectangle rect1 = new Rectangle(new Point(0, 0), new Size(e.ClipRectangle.Width, 1));
            Rectangle rect2 = new Rectangle(new Point(0, e.ClipRectangle.Height - 1), new Size(e.ClipRectangle.Width, 1));
            LinearGradientBrush linearGradientBrush1 = new LinearGradientBrush(rect1, Color.White, SystemColors.ControlDark, LinearGradientMode.Horizontal);
            LinearGradientBrush linearGradientBrush2 = new LinearGradientBrush(rect2, Color.White, SystemColors.ControlDark, LinearGradientMode.Horizontal);
            linearGradientBrush1.SetBlendTriangularShape(0.5f);
            linearGradientBrush2.SetBlendTriangularShape(0.5f);
            e.Graphics.FillRectangle(linearGradientBrush1, rect1);
            e.Graphics.FillRectangle(linearGradientBrush2, rect2);
            linearGradientBrush1.Dispose();
            linearGradientBrush2.Dispose();
        }

        public static void paintBottomPanel(PaintEventArgs e)
        {
            if (e.ClipRectangle == Rectangle.Empty)
                return;
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(e.ClipRectangle, Color.FromArgb(246, 219, 128), Color.FromArgb(254, 251, 200), LinearGradientMode.Vertical);
            e.Graphics.FillRectangle(linearGradientBrush, e.ClipRectangle);
            e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, 0), new Point(e.ClipRectangle.Width, 0));
        }

        public static Image shrinkImage(Image originalImage, Size newSize)
        {
            Bitmap bitmap = new Bitmap(newSize.Width, newSize.Height);
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.InterpolationMode = InterpolationMode.High;
                graphics.DrawImage(originalImage, new Rectangle(new Point(0, 0), newSize));
            }
            return bitmap;
        }

        public static Color DarkenColor(Color color, int Factor)
        {
            if (Factor < 0 | Factor > byte.MaxValue)
                Factor = 1;
            int num1 = color.R;
            int num2 = color.G;
            int num3 = color.B;
            int red = num1 - Factor;
            int green = num2 - Factor;
            int blue = num3 - Factor;
            if (red < 0)
                red = 0;
            if (green < 0)
                green = 0;
            if (blue < 0)
                blue = 0;
            return Color.FromArgb(red, green, blue);
        }

        public static Image ConvertBlackAndWhite(Image image)
        {
            Image original = image;
            ImageFormat rawFormat = original.RawFormat;
            Bitmap bitmap1 = new Bitmap(original, original.Width, original.Height);
            Bitmap bitmap2 = new Bitmap(bitmap1.Width, bitmap1.Height);
            Graphics graphics = Graphics.FromImage(bitmap2);
            float[][] newColorMatrix1 = new float[5][]
      {
        new float[5]
        {
          0.3f,
          0.3f,
          0.3f,
          0.0f,
          0.0f
        },
        new float[5]
        {
          0.59f,
          0.59f,
          0.59f,
          0.0f,
          0.0f
        },
        new float[5]
        {
          0.11f,
          0.11f,
          0.11f,
          0.0f,
          0.0f
        },
        null,
        null
      };
            float[][] numArray1 = newColorMatrix1;
            int index1 = 3;
            float[] numArray2 = new float[5];
            numArray2[3] = 1f;
            float[] numArray3 = numArray2;
            numArray1[index1] = numArray3;
            float[][] numArray4 = newColorMatrix1;
            int index2 = 4;
            float[] numArray5 = new float[5];
            numArray5[4] = 1f;
            float[] numArray6 = numArray5;
            numArray4[index2] = numArray6;
            ColorMatrix newColorMatrix2 = new ColorMatrix(newColorMatrix1);
            ImageAttributes imageAttr = new ImageAttributes();
            imageAttr.SetColorMatrix(newColorMatrix2);
            graphics.DrawImage(bitmap1, new Rectangle(0, 0, bitmap1.Width, bitmap1.Height), 0, 0, bitmap1.Width, bitmap1.Height, GraphicsUnit.Pixel, imageAttr);
            graphics.Dispose();
            return bitmap2;
        }
    }
}
