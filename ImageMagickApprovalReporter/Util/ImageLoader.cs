using System;
using System.Net.Cache;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ImageMagickApprovalReporter.Util
{
    internal class ImageLoader
    {
        public void LoadImage(Image image, string filePath)
        {
            try
            {
                image.Source = LoadImageFile(filePath);
            }
            catch
            {
            }
        }

        private ImageSource LoadImageFile(string filePath = null)
        {
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.CacheOption = BitmapCacheOption.None;
            bitmap.UriCachePolicy = new RequestCachePolicy(RequestCacheLevel.BypassCache);
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
            if (filePath != null)
                bitmap.UriSource = new Uri(filePath, UriKind.RelativeOrAbsolute);
            bitmap.EndInit();

            return bitmap;
        }
    }
}