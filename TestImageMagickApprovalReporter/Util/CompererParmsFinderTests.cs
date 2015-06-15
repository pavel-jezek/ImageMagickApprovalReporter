using ImageMagickApprovalReporter.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageMagickApprovalReporter.Util.Tests
{
    [TestClass()]
    public class CompererParmsFinderTests
    {
        [TestMethod()]
        public void FindIImageCompererParms()
        {
            var parmsObjects = CompererParmsFinder.CreateCompererParmsDictionary();
            Assert.AreEqual(2, parmsObjects.Count);
        }
    }
}