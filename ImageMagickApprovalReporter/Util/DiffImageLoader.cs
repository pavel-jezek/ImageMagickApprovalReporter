using ApprovalUtilities.Utilities;
using ImageMagickApprovalReporter.Comperers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ImageMagickApprovalReporter.Util
{
    internal class DiffImageLoader
    {
        private readonly ImageCompererFactory compererFactory;
        private readonly ImageLoader imageLoader = new ImageLoader();

        public DiffImageLoader(ImageCompererFactory compererFactory)
        {
            this.compererFactory = compererFactory;
        }

        public void LoadDiffIamge(Image DiffImage, string DiffFilePath, string RecivedFilePath, string ApprovedFilePath, IImageCompererParms CurrnentParms)
        {
            using (TempFile tmp = new TempFile(DiffFilePath)) //remove the file after using
            using (var comperer = compererFactory.Create(RecivedFilePath, ApprovedFilePath, CurrnentParms))
            {
                comperer.SaveDiffIamgeTo(DiffFilePath);
                if (!comperer.NoFileExists)
                    imageLoader.LoadImage(DiffImage, DiffFilePath);
            }
        }
    }
}