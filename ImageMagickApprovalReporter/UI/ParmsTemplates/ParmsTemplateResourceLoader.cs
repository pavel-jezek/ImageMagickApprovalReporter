using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ImageMagickApprovalReporter.UI.ParmsTemplates
{
    internal class ParmsTemplateResourceLoader
    {
        private ParmsTemplatesList internalTemplateList = new ParmsTemplatesList();

        public void LoadResourcesTo(ResourceDictionary resources)
        {
            foreach (var uri in internalTemplateList.TempaltesUris)
            {
                ResourceDictionary resource2 = new ResourceDictionary
                {
                    Source = uri
                };
                resources.MergedDictionaries.Add(resource2);
            }
        }
    }
}