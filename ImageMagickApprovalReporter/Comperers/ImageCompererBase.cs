using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.Comperers
{
    public abstract class ImageCompererBase : IDisposable
    {
        protected readonly string image1Path;
        protected readonly string image2Path;
        protected readonly MagickImage image1;
        protected readonly MagickImage image2;

        public ImageCompererBase(string image1Path, string image2Path)
        {
            this.image1Path = image1Path;
            this.image2Path = image2Path;

            if (!File.Exists(image1Path) || !File.Exists(image2Path))
            {
                NoFileExists = true;
                return;
            }
            image1 = TransferToRgbIfNeeded(new MagickImage(image1Path));
            image2 = TransferToRgbIfNeeded(new MagickImage(image2Path));
        }

        public virtual bool NoFileExists { get; protected set; }

        public abstract void SaveDiffIamgeTo(string diffImagePath);

        public virtual void Dispose()
        {
            if (image1 != null)
                image1.Dispose();
            if (image2 != null)
                image2.Dispose();
        }

        protected virtual MagickImage TransferToRgbIfNeeded(MagickImage image)
        {
            if (image.ColorSpace != ColorSpace.sRGB)
                image.ColorSpace = ColorSpace.sRGB;
            return image;
        }
    }

    internal abstract class ImageCompererBase<ParmsType> : ImageCompererBase where ParmsType : IImageCompererParms
    {
        protected readonly ParmsType parms;

        public ImageCompererBase(string image1Path, string image2Path, ParmsType parms)
            : base(image1Path, image2Path)
        {
            this.parms = parms;
        }
    }
}