namespace Factorio.NET.Types
{
    public class Color : FactorioType
    {
        public float R { get; }

        public float G { get; }

        public float B { get; }

        public float A { get; }

        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }
}