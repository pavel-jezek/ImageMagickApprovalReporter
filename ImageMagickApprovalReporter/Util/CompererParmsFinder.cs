using ImageMagickApprovalReporter.Comperers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.Util
{
    internal static class CompererParmsFinder
    {
        public static Dictionary<string, IImageCompererParms> CreateCompererParmsDictionary()
        {
            var objects =
                Util.ReflectionHelper.CreateAllClassesInAssemblyImplementing<IImageCompererParms>();
            return objects.ToDictionary(o => o.Name);
        }

        public static IEnumerable<Type> FindCompererParmsTypes()
        {
            return Util.ReflectionHelper.FindAllClassesInAssemblyImplementing<IImageCompererParms>();
        }
    }
}