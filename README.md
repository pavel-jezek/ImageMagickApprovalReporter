# ImageMagickApprovalReporter
A image reporter to be used with [ApprovalTests.Net](https://github.com/approvals/ApprovalTests.Net)

ApprovalTest has several built-in bindings to reporters you can use.
One of them which is very nice is [Perforce P4Merge](http://www.perforce.com/product/components/perforce-visual-merge-and-diff-tools).
But for me it has several limitations, the foremost is that there is no way to approve an image.

Usage
---
Just add `[UseReporter(typeof(IMImageReporter)]` attribute on the test class or test method. See the [AprrovalTests documentation](http://blog.approvaltests.com/2011/12/using-reporters-in-approval-tests.html)

NuGet Availabilty
---
A Nuget package will come soon.

Example
---
```c#
    [TestClass, UseReporter(typeof(IMImageReporter))]
    public class ImageReporterTests
    {
        [TestMethod]
        public void TestImage()
        {
            string filePath=@"c:\myFile.png";

            SUT.DoSomthingToFile(filePath);

            Approvals.VerifyFile(filePath);
        }
    }
```

ScreenShots
---
**At first, there is nothing to compare to, so just approve it if it is the right file**

![](https://github.com/zivni/ImageMagickApprovalReporter/blob/master/ReadmeResources/ScreenShot0.jpg)

**Compare using the outline method**

![](https://github.com/zivni/ImageMagickApprovalReporter/blob/master/ReadmeResources/ScreenShot1.jpg)

**Compare using the absolute method**

![](https://github.com/zivni/ImageMagickApprovalReporter/blob/master/ReadmeResources/ScreenShot2.jpg)

**When the image attributes are different, the differences are highlighted**

![](https://github.com/zivni/ImageMagickApprovalReporter/blob/master/ReadmeResources/ScreenShot3.jpg)

Contributions
---
If you are good at ImageMagick and can make the Comparsions better, please help.

License
---
[MIT License](https://raw.githubusercontent.com/zivni/ImageMagickApprovalReporter/master/LICENSE.md)
