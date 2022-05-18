using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows;

namespace BotHelper
{
    public static class PictureManager
    {
        public static void Test()
        {
            // get screenshot into bitmap
            double height = SystemParameters.FullPrimaryScreenHeight; // Presentation.dll jest z plików w programfiles, trzebaby dołączyć dll do projektu
            double width = SystemParameters.FullPrimaryScreenWidth;
            // var bmp = new Bitmap((int)width, (int)height, PixelFormat.Format32bppArgb);
            var bmp = new Bitmap((int)width, (int)height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
          //  bmp.Save("test.png", ImageFormat.Png); // png jest bezstratnym zapisem, nie tworzy artefaktów

            var lookForBmp = new Bitmap("LookFor.png"); // bmp szukanego wzorca

            var matchPixelCountNeeded = (lookForBmp.Height * lookForBmp.Width) * 0.7;

            var findIt = false;
            var widthStepSize = (int)(lookForBmp.Width * 0.09);
            var heighStepSize = (int)(lookForBmp.Height * 0.09);
            var stepCounter = 0;
            int? findX = null;
            int? findY = null;
            for (var i = 0; i < bmp.Width - lookForBmp.Width; i+= widthStepSize)
                for (var j = 0; j < bmp.Height - lookForBmp.Height; j+= heighStepSize)
                {
                    if (findIt)
                    {
                        break;
                    }

                    var rect = new Rectangle(i, j, lookForBmp.Width, lookForBmp.Height);
                    var subBmp = bmp.Clone(rect, bmp.PixelFormat);

                    var matchCounter = 0;
                    for (var w = 0; w < subBmp.Width; w ++)
                        for (var h = 0; h < subBmp.Height; h++)
                        {
                            if (findIt)
                            {
                                break;
                            }

                            stepCounter++;
                            if (subBmp.GetPixel(w, h) == lookForBmp.GetPixel(w, h))
                            {
                                matchCounter++;
                            }
                        }

                    if (matchCounter > matchPixelCountNeeded)
                    {
                        findX = i;
                        findY = j;
                        findIt = true;
                    }
                }

            if (findIt)
            {
                var lol = stepCounter;
                MessageBox.Show("Got It!");
            }
            else
            {
                MessageBox.Show("RIP :C");
            }
            //  bmp.GetPixel()

        }
    }
}
