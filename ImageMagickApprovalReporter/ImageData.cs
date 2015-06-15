using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter
{
    internal class ImageData
    {
        public ImageData()
        {
            Diff = new HashSet<string>();
        }

        public string FilePath { get; set; }

        public string ImageFormat { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int VerticalResolution { get; set; }

        public int HorizontalResolution { get; set; }

        public string ColorSpace { get; set; }

        public long FileSizeInBytes { get; set; }

        public HashSet<string> Diff { get; set; }

        public static ImageData FromImage(MagickImage image)
        {
            if (image == null)
                return null;

            return new ImageData
            {
                FilePath = image.FileName,
                ImageFormat = image.FormatInfo.Format.ToString(),
                Width = image.Width,
                Height = image.Height,
                HorizontalResolution = Convert.ToInt32(image.ResolutionX),
                VerticalResolution = Convert.ToInt32(image.ResolutionY),
                ColorSpace = image.ColorSpace.ToString(),
                FileSizeInBytes = image.FileSize,
            };
        }

        public static ImageData FromImage(string filePath)
        {
            try
            {
                using (MagickImage image = new MagickImage(filePath))
                {
                    return FromImage(image);
                }
            }
            catch
            {
                return null;
            }
        }

        public void SetDiffFromAnother(ImageData other)
        {
            if (other == null)
                return;

            var props = typeof(ImageData).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.PropertyType.Name == "String" || !prop.PropertyType.IsClass)
                {
                    var val1 = prop.GetValue(this, null);
                    var val2 = prop.GetValue(other, null);
                    if (!val1.Equals(val2))
                    {
                        this.Diff.Add(prop.Name);
                        other.Diff.Add(prop.Name);
                    }
                }
            }
        }
    }
}