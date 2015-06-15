using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.Comperers
{
    internal class OutlineImageComperer : ImageCompererBase<OutlineImageCompererParms>
    {
        public OutlineImageComperer(string image1Path, string image2Path, OutlineImageCompererParms parms)
            : base(image1Path, image2Path, parms)
        {
        }

        public override void SaveDiffIamgeTo(string diffImagePath)
        {
            if (base.NoFileExists)
                return;

            var img1 = ChangeImage(image1);
            var img2 = ChangeImage(image2);

            MagickImage diffImage = new MagickImage();

            img1.SetHighlightColor(new MagickColor(parms.HighLightColor));

            img1.Compare(img2, ErrorMetric.Absolute, diffImage);

            diffImage.Despeckle();

            MakeSemiTranspernt(image2);

            if (parms.ShowImages)
            {
                image1.Composite(image2, CompositeOperator.Atop);
                if (parms.HighLightColor != "None")
                    image1.Composite(diffImage, CompositeOperator.Atop);
                image1.Write(diffImagePath);
            }
            else
            {
                diffImage.Write(diffImagePath);
            }
        }

        private void MakeSemiTranspernt(MagickImage image)
        {
            double opacitySetting = Quantum.Max / (Quantum.Max * 0.5);
            image.Alpha(AlphaOption.Set);
            image.Evaluate(Channels.Alpha, EvaluateOperator.Min, Quantum.Max / opacitySetting);
        }

        private MagickImage ChangeImage(MagickImage srcImage)
        {
            var image = srcImage.Clone();

            image.BackgroundColor = MagickColor.Transparent;
            image.ColorSpace = ColorSpace.GRAY;
            image.Edge(1);
            image.Normalize();
            image.Threshold(50);
            image.Despeckle();
            image1.RePage();

            return image;
        }
    }

    internal class OutlineImageCompererParms : HighLightColorParms
    {
        protected bool _ShowImages = true;

        public override string Name
        {
            get { return "Outline"; }
        }

        public bool ShowImages
        {
            get { return _ShowImages; }
            set
            {
                if (value != _ShowImages)
                {
                    _ShowImages = value;
                    RaisePropertyChanged("ShowImages");
                }
            }
        }

        public override string[] AvailbleColors
        {
            get
            {
                return base.AvailbleColors.Concat(new string[] { "None" }).ToArray();
            }
        }
    }
}