using ImageMagickApprovalReporter.Comperers;
using ImageMagickApprovalReporter.UI.ParmsTemplates;
using ImageMagickApprovalReporter.Util;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace ImageMagickApprovalReporter.UI
{
    internal partial class MainWindow : Window, INotifyPropertyChanged
    {
        protected IImageCompererParms _CurrnentParms;

        private readonly ImageLoader imageLoader = new ImageLoader();
        private readonly DiffImageLoader diffImageLoader;

        internal MainWindow(string approvedFilePath, string recivedFilePath, ImageCompererFactory compererFactory)
        {
            this.ApprovedFilePath = approvedFilePath;
            this.RecivedFilePath = recivedFilePath;
            this.diffImageLoader = new DiffImageLoader(compererFactory);

            InitializeComponent();

            this.Title = "Approve - " + System.IO.Path.GetFileName(approvedFilePath);

            LoadParms();
            LoadIamges(approvedFilePath, recivedFilePath);
            LoadImagesData(approvedFilePath, recivedFilePath);

            DataContext = this;
        }

        public Dictionary<string, IImageCompererParms> AvailableParms { get; set; }

        public IImageCompererParms CurrnentParms
        {
            get { return _CurrnentParms; }
            set
            {
                if (value != _CurrnentParms)
                {
                    if (_CurrnentParms != null)
                        _CurrnentParms.PropertyChanged += _CurrnentParms_PropertyChanged;
                    _CurrnentParms = value;
                    _CurrnentParms.PropertyChanged += _CurrnentParms_PropertyChanged;
                    LoadDiffIamge();
                    RaisePropertyChanged("CurrnentParms");
                }
            }
        }

        public string ApprovedFilePath { get; set; }

        public string RecivedFilePath { get; set; }

        public string DiffFilePath
        {
            get
            {
                var extention = System.IO.Path.GetExtension(ApprovedFilePath);
                var tmpFile = System.IO.Path.ChangeExtension(ApprovedFilePath, "diff" + ".png");
                return tmpFile;
            }
        }

        private void Approve_Click(object sender, RoutedEventArgs e)
        {
            File.Copy(RecivedFilePath, ApprovedFilePath, true);
            this.Close();
        }

        private void LoadImagesData(string approvedFilePath, string recivedFilePath)
        {
            var recivedIamgeData = ImageData.FromImage(recivedFilePath);
            var approvedIamgeData = ImageData.FromImage(approvedFilePath);
            recivedIamgeData.SetDiffFromAnother(approvedIamgeData);

            RecivedImageDetiails.Data = recivedIamgeData;
            ApprovedImageDetiails.Data = approvedIamgeData;
        }

        private void LoadIamges(string approvedFilePath, string recivedFilePath)
        {
            imageLoader.LoadImage(RecivedImage, recivedFilePath);
            imageLoader.LoadImage(ApprovedImage, approvedFilePath);
        }

        private void LoadParms()
        {
            AvailableParms = CompererParmsFinder.CreateCompererParmsDictionary();
            CurrnentParms = AvailableParms["Outline"];

            new ParmsTemplateResourceLoader().LoadResourcesTo(ParmsConntentPresenter.Resources);
        }

        private void _CurrnentParms_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            LoadDiffIamge();
        }

        private void LoadDiffIamge()
        {
            diffImageLoader.LoadDiffIamge(DiffImage, DiffFilePath, RecivedFilePath, ApprovedFilePath, CurrnentParms);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string propertyName)
        {
            var temp = PropertyChanged;
            if (temp != null)
                temp(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}