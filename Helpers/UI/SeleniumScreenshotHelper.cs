using OpenQA.Selenium;
using System;
using TTCBDD.Settings;

namespace TTCBDD.Helpers.UI
{
    class SeleniumScreenshotHelper
    {
        public static void TakeScreenshot(string filename = "Screen")
        {
            Screenshot ss = ((ITakesScreenshot)ObjectRepository.Driver).GetScreenshot();

            if (filename.Equals("Screen"))
            {
                filename = filename + DateTime.UtcNow.ToString("yyyy-MM-dd-mm-ss") + ".jpg";
            }

            if (filename.Substring(filename.Length - 4, 4) != ".jpg")
            {
                filename = filename + ".jpg";
            }

            ss.SaveAsFile(filename, ScreenshotImageFormat.Jpeg);
        }
    }
}
