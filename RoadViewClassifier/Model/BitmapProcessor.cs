using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace RoadViewClassifier.Model
{
    public static class BitmapProcessor
    {
        static private Bitmap ProcessPixels(Bitmap original, Func<Color, Color> action)
        {
            Bitmap bmp = (Bitmap)original.Clone();

            foreach (var x in Enumerable.Range(0, bmp.Width - 1))
                foreach (var y in Enumerable.Range(0, bmp.Height - 1))
                    bmp.SetPixel(x, y, action(bmp.GetPixel(x, y)));

            return bmp;
        }
        

        static public Bitmap ProportionMask(Bitmap original, Color sample, double accuracy)
        {
            ProportionRGB proportion = new ProportionRGB(sample);
            Func<Color, Color> func =
                col => new ProportionRGB(col).ApproximatelyEqualTo(proportion, accuracy) ?
                Color.White : Color.Black;

            return ProcessPixels(original, func);
        }
    }
}
