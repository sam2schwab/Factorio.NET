using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Factorio.NET.Types
{
    public class IconSpecification : FactorioType
    {
        public ReadOnlyCollection<IconData> Icons { get; }
        
        public IconSpecification(List<IconData> icons)
        {
            Icons = new ReadOnlyCollection<IconData>(icons);
        }
    }
}