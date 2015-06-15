using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.Comperers
{
    public interface IImageCompererParms : INotifyPropertyChanged
    {
        string Name { get; }
    }
}