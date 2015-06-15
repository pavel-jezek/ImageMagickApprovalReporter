using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.Comperers
{
    internal class ImageComperer : ImageCompererBase<ImageCompererParms>
    {
        public ImageComperer(string image1Path, string image2Path, ImageCompererParms parms)
            : base(image1Path, image2Path, parms)
        {
        }

        public override void SaveDiffIamgeTo(string diffImagePath)
        {
            if (NoFileExists)
                return;

            MagickImage diffImage = new MagickImage();

            image1.SetHighlightColor(new MagickColor(parms.HighLightColor));
            image1.Compose = parms.Operator;
            image1.Compare(image2, ErrorMetric.Absolute, diffImage);

            diffImage.Write(diffImagePath);
        }
    }

    internal class ImageCompererParms : HighLightColorParms
    {
        protected CompositeOperator _Operator;

        public override string Name
        {
            get { return "Absolute"; }
        }

        public CompositeOperator[] AvailbleOperators
        {
            get { return Enum.GetValues(typeof(CompositeOperator)).Cast<CompositeOperator>().ToArray(); }
        }

        public CompositeOperator Operator
        {
            get { return _Operator; }
            set
            {
                if (value != _Operator)
                {
                    _Operator = value;
                    RaisePropertyChanged("Operator");
                }
            }
        }
    }
}