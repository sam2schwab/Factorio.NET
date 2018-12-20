namespace Factorio.NET.Types
{
    public class IconData : FactorioType
    {
        public string Icon { get; }

        public int? IconSize { get; internal set; }

        public Color Tint { get; }

        public int[] Shift { get; }

        public double? Scale { get; }

        public IconData(string icon, int? iconSize = null, Color tint = null, int[] shift = null, double? scale = null)
        {
            Icon = icon;
            IconSize = iconSize;
            Tint = tint;
            Shift = shift;
            Scale = scale;
        }
    }
}