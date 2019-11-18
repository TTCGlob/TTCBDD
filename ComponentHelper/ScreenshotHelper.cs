using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTCBDD.Settings;

namespace TTCBDD.ComponentHelper
{
    class ScreenshotHelper
    {
        public static void TakeScreenshot(String filename = "Screen")
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
