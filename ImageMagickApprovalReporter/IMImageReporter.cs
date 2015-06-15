using ApprovalTests.Core;
using ImageMagickApprovalReporter.Comperers;
using ImageMagickApprovalReporter.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter
{
    public class IMImageReporter : IApprovalFailureReporter
    {
        public static readonly IMImageReporter INSTANCE = new IMImageReporter();

        static IMImageReporter()
        {
            CompererFactory = new ImageCompererFactory();
        }

        internal static ImageCompererFactory CompererFactory { get; set; }

        public void Report(string approved, string received)
        {
            MainWindow mainWindow = new MainWindow(approved, received, CompererFactory);
            mainWindow.ShowDialog();
        }
    }
}