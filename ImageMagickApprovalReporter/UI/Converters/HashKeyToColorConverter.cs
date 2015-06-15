using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ImageMagickApprovalReporter.UI.Converters
{
    internal class HashKeyToColorConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var hashSet = value as HashSet<string>;
            var strParm = parameter as string;
            if (hashSet != null && strParm != null && hashSet.Contains(strParm))
                return new SolidColorBrush(Colors.PaleVioletRed);

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}