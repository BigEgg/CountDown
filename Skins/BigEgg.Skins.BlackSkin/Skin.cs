using System;
using System.Windows.Media.Imaging;
using BigEgg.Skins.BlackSkin.Properties;
using System.IO;

namespace BigEgg.Skins
{
    public class Skin : SkinBase
    {
        public Skin()
            : base(Resources.Name, Resources.Description, Resources.Demo)
        {
        }
    }
}
