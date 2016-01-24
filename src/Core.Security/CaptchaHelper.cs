using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace eSIS.Core.Security
{
    public static class CaptchaHelper
    {
        public static byte[] GetCaptchaImage(int width, int height, string captchaString)
        {
            byte[] returnValue;
            var bmp = MakeCaptcha(width, height, captchaString);

            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, ImageFormat.Jpeg);
                returnValue = ms.GetBuffer();
                ms.Close();
            }
            return returnValue;
        }

        private static Bitmap MakeCaptcha(int width, int height, string captchaString)
        {

            var fontname = "";
            const int nPoints = 8;
            var randomGenerator = new Random();
            var imgFontcolor = Brushes.Black;

            var bmp = new Bitmap(width, height, PixelFormat.Format16bppRgb555);
            var rect = new Rectangle(0, 0, width, height);


            var sFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var g = Graphics.FromImage(bmp);


            // Set up the text font.
            SizeF size;
            float fontSize = 45;
            Font font;

            // try to use requested font, but
            // If the named font is not installed, default to a system font.
            try
            {
                font = new Font("Lucida Console", fontSize);
                font.Dispose();
            }
            catch (Exception)
            {
                fontname = FontFamily.GenericSerif.Name;
            }


            // build a new string with space through the chars
            // i.e. keyword 'hello' become 'h e l l o '
            var tempKey = "";

            foreach (var t in captchaString)
            {
                tempKey = String.Concat(tempKey, t.ToString());
                tempKey = String.Concat(tempKey, " ");
            }

            // Adjust the font size until the text fits within the image.
            do
            {
                fontSize--;
                font = new Font(fontname, fontSize, FontStyle.Regular);
                size = g.MeasureString(tempKey, font);
            } while (size.Width > (0.8 * bmp.Width));


            g.Clear(Color.Silver); // blank the image
            g.SmoothingMode = SmoothingMode.AntiAlias; // antialias objects

            // fill with a liner gradient
            // random colors
            g.FillRectangle(
                new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(bmp.Width, bmp.Height),
                    Color.FromArgb(
                        255, //randomGenerator.Next(255),
                        randomGenerator.Next(255),
                        randomGenerator.Next(255),
                        randomGenerator.Next(255)
                        ),
                    Color.FromArgb(
                        randomGenerator.Next(100),
                        randomGenerator.Next(255),
                        randomGenerator.Next(255),
                        randomGenerator.Next(255)
                        )),
                rect);

            // apply swirl filter
            //BitmapFilter.Swirl(bmp, randomGenerator.NextDouble(), true);

            // Add some random noise.
            var hatchBrush = new HatchBrush(
                HatchStyle.LargeConfetti,
                Color.LightGray,
                Color.DarkGray);

            var m = Math.Max(rect.Width, rect.Height);
            for (var i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                var x = randomGenerator.Next(rect.Width);
                var y = randomGenerator.Next(rect.Height);
                var w = randomGenerator.Next(m / 50);
                var h = randomGenerator.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }


            // write keyword

            // keyword positioning
            // to space equally
            var deltax = Convert.ToInt32(size.Width / tempKey.Length);
            var posx = Convert.ToInt32((width - size.Width) / 2);

            // write each keyword char
            for (var l = 0; l < tempKey.Length; l++)
            {
                var posy = ((int)(2.5 * (bmp.Height / 5))) +
                           (((l % 2) == 0) ? -2 : 2) * ((int)(size.Height / 3));
                posy = (int)((bmp.Height / 2) + (size.Height / 2));
                posy += (int)((((l % 2) == 0) ? -2 : 2) * ((size.Height / 3)));
                posx += deltax;
                g.DrawString(tempKey[l].ToString(),
                    font,
                    imgFontcolor,
                    posx,
                    posy,
                    sFormat);
            }


            // draw a curve 
            var ps = new Point[nPoints];

            for (var ii = 0; ii < nPoints; ii++)
            {
                var x = randomGenerator.Next(bmp.Width);
                var y = randomGenerator.Next(bmp.Height);
                ps[ii] = new Point(x, y);
            }

            g.DrawCurve(new Pen(imgFontcolor, 2), ps, Convert.ToSingle(randomGenerator.NextDouble()));

            // apply water filter
            //BitmapFilter.Water(bmp, nWave, false);

            // Clean up.
            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            return bmp;
        }
    }
}