using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.Comperers
{
    internal abstract class HighLightColorParms : NotifiedClass, IImageCompererParms
    {
        protected string _HighLightColor;

        public HighLightColorParms()
        {
            HighLightColor = "Red";
        }

        public abstract string Name { get; }

        public string HighLightColor
        {
            get { return _HighLightColor; }
            set
            {
                if (value != _HighLightColor)
                {
                    _HighLightColor = value;
                    RaisePropertyChanged("HighLightColor");
                }
            }
        }

        public virtual string[] AvailbleColors
        {
            get
            {
                return new string[]
                    {
                        "Red","Blue","Green","Yellow", "Transparent"
                    };
            }
        }
    }
}