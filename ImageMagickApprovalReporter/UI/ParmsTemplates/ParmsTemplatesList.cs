using ImageMagickApprovalReporter.Comperers;
using ImageMagickApprovalReporter.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.UI.ParmsTemplates
{
    internal class ParmsTemplatesList
    {
        private const string dllName = "ImageMagickApprovalReporter";
        private const string temlateFilePath = "UI/ParmsTemplates";

        public ParmsTemplatesList()
        {
            var CompererParmsTypes = CompererParmsFinder.FindCompererParmsTypes();
            TempaltesUris = new List<Uri>();
            foreach (var type in CompererParmsTypes)
            {
                AddToList(type);
            }
        }

        public List<Uri> TempaltesUris { get; private set; }

        private void AddToList<T>(string uriString)
        {
            AddToList(typeof(T));
        }

        private void AddToList(Type t)
        {
            AddToList(BuildDeafultUrlString(t.Name + ".xaml"));
        }

        private void AddToList(string uriString)
        {
            TempaltesUris.Add(new Uri(uriString, UriKind.RelativeOrAbsolute));
        }

        private string BuildDeafultUrlString(string resoureceFileName)
        {
            return string.Format("/{0};;component/{1}/{2}", dllName, temlateFilePath, resoureceFileName);
        }
    }
}