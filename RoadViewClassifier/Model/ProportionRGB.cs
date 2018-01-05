using System.Drawing;

namespace RoadViewClassifier.Model
{
    class ProportionRGB
    {
        public double ProportionR { get; private set; }
        public double ProportionG { get; private set; }
        public double ProportionB { get; private set; }

        public ProportionRGB(Color color)
        {
            int sumRGB = color.R + color.G + color.B;

            ProportionR = sumRGB != 0 ? (double) color.R / sumRGB : 1.0 / 3;
            ProportionG = sumRGB != 0 ? (double) color.G / sumRGB : 1.0 / 3;
            ProportionB = sumRGB != 0 ? (double) color.B / sumRGB : 1.0 / 3;
        }

        public bool ApproximatelyEqualTo(ProportionRGB other, double accuracy)
        {
            if (this.ProportionR < other.ProportionR - accuracy || this.ProportionR > other.ProportionR + accuracy)
                return false;
            if (this.ProportionG < other.ProportionG - accuracy || this.ProportionG > other.ProportionG + accuracy)
                return false;
            if (this.ProportionB < other.ProportionB - accuracy || this.ProportionB > other.ProportionB + accuracy)
                return false;

            return true;
        }
    }
}
