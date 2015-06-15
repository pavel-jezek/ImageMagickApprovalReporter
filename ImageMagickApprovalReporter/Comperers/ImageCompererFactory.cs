using ImageMagickApprovalReporter.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.Comperers
{
    internal class ImageCompererFactory
    {
        public virtual ImageCompererBase Create(string image1Path, string image2Path, IImageCompererParms parms)
        {
            var comprerType = GetCompererTypeForParms(parms);

            var comperer = Activator.CreateInstance(comprerType, image1Path, image2Path, parms);
            return comperer as ImageCompererBase;
        }

        private static Type GetCompererTypeForParms(IImageCompererParms parms)
        {
            Type[] types = Assembly.GetExecutingAssembly().GetTypes();
            var compererTypes = types
                .Where(t => t.IsSubclassOf(typeof(ImageCompererBase))
                    && !t.IsAbstract
                    && t.GetConstructor(new Type[] { typeof(string), typeof(string), parms.GetType() }) != null);

            var comprerType = compererTypes.First();
            return comprerType;
        }

        private IEnumerable<Type> FindCompererTypes()
        {
            return ReflectionHelper.FindAllClassesInAssemblyInhertidedBy<ImageCompererBase>();
        }
    }
}