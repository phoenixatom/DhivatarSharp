using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace DhivatarGenerator
{
    public class Dhivatar
    {
        private const int MIN_RENDER_SIZE = 512;

        /// <summary>
        /// Generates a dhivehi avatar image based on the input string.
        /// </summary>
        /// <param name="input">The input string to generate the dhivatar from.</param>
        /// <param name="size">The size of the dhivatar image.</param>
        /// <param name="bgColor">The background color of the dhivatar.</param>
        /// <param name="fontColor">The font color of the text.</param>
        /// <param name="fontName">The name of the font file to use.</param>
        /// <param name="fileType">The file type of the generated image (e.g., "PNG" or "JPEG").</param>
        /// <returns>The generated dhivatar image as a byte array.</returns>
        public static byte[] Generate(string input, int size = 150, Color? bgColor = null, Color? fontColor = null, string fontName = "mv_eamaan_xp.otf", string fileType = "PNG")
        {
            int renderSize = Math.Max(size, MIN_RENDER_SIZE);

            if (!bgColor.HasValue)
            {
                bgColor = BackgroundColor(input.GetHashCode());
            }

            if (!fontColor.HasValue)
            {
                fontColor = Color.White; // Default font color
            }

            using (var image = new Bitmap(renderSize, renderSize))
            using (var graphics = Graphics.FromImage(image))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;

                using (var font = LoadFont(renderSize, fontName))
                using (var brush = new SolidBrush(fontColor.Value))
                {
                    string text = GetText(input);
                    PointF textPosition = GetTextPosition(renderSize, text, font);

                    graphics.Clear(bgColor.Value);
                    graphics.DrawString(text, font, brush, textPosition);

                    using (var stream = new MemoryStream())
                    {
                        image.Save(stream, GetImageFormat(fileType));
                        return stream.ToArray();
                    }
                }
            }
        }

        private static Color BackgroundColor(int seed)
        {
            var random = new Random(seed);
            int r, g, b;
            do
            {
                r = random.Next(256);
                g = random.Next(256);
                b = random.Next(256);
            } while (r + g + b > 255 * 2);

            return Color.FromArgb(r, g, b);
        }

        private static Font LoadFont(int size, string fontName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", fontName);
            return new Font(path, 0.8f * size);
        }

        private static string GetText(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "#";
            }
            else if (input.Contains(" "))
            {
                return input.Split(' ')[1][0].ToString();
            }
            else
            {
                return input[0].ToString();
            }
        }

        private static PointF GetTextPosition(int size, string text, Font font)
        {
            using (var image = new Bitmap(size, size))
            using (var graphics = Graphics.FromImage(image))
            {
                // Measure the text size using the Graphics object
                SizeF textSize = graphics.MeasureString(text, font);

                // Calculate the left position to center horizontally
                float left = (size - textSize.Width) / 2.0f;

                // Calculate the top position for vertical centering or adjust as needed
                float top = (size - textSize.Height) / 2.0f; // Center vertically

                return new PointF(left, top);
            }
        }

        private static ImageFormat GetImageFormat(string fileType)
        {
            switch (fileType.ToLower())
            {
                case "png":
                    return ImageFormat.Png;
                case "jpeg":
                case "jpg":
                    return ImageFormat.Jpeg;
                default:
                    throw new NotSupportedException($"File type {fileType} is not supported.");
            }
        }
    }
}
