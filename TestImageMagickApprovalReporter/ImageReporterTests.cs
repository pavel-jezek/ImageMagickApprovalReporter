using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalUtilities.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace ImageMagickApprovalReporter.Tests
{
    [TestClass, UseReporter(typeof(IMImageReporter))]
    public class ImageReporterTests
    {
        private static string solutiondirecory = PathUtilities.GetDirectoryForCaller();
        private static string imagesDirectory = solutiondirecory + @"Images\";
        private string generatedApprovedFilePath;

        [TestCleanup]
        public void _Cleanup()
        {
            if (generatedApprovedFilePath != null)
                File.Delete(generatedApprovedFilePath);
        }

        [TestMethod]
        public void WHEN_approved_file_exists_THEN_show_all()
        {
            TestTwoFiles("testSource1.jpg", "testApproved.jpg");
        }

        [TestMethod]
        public void WHEN_approved_file_exists_2_THEN_show_all()
        {
            TestTwoFiles("testSource2.jpg", "testApproved.jpg");
        }

        [TestMethod]
        public void WHEN_approved_file_have_diffrent_Properties_THEN_highlight_diffrent_values()
        {
            TestTwoFiles("testSource3.jpg", "testApproved.jpg");
        }

        [TestMethod]
        public void WHEN_approved_file_doesnt_exists_THEN_show_only_recived()
        {
            string fileName = imagesDirectory + "test.jpg";
            File.Copy(imagesDirectory + "testSource1.jpg", fileName, true);
            SetGeneratedApprovedFilePath();
            File.Delete(generatedApprovedFilePath);

            Approvals.VerifyFile(fileName);
        }

        private void TestTwoFiles(string srcFile, string approvedFile)
        {
            string fileName = imagesDirectory + "test.jpg";
            string srcApprovedFilePath = imagesDirectory + approvedFile;
            SetGeneratedApprovedFilePath();
            File.Copy(imagesDirectory + srcFile, fileName, true);
            File.Copy(srcApprovedFilePath, generatedApprovedFilePath, true);

            Approvals.VerifyFile(fileName);
        }

        private void SetGeneratedApprovedFilePath()
        {
            generatedApprovedFilePath = solutiondirecory + Approvals.GetDefaultNamer().Name + ".approved.jpg";
        }
    }
}