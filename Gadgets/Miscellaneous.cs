using GadgetCore.API;
using TienContentMod.Items;

namespace TienContentMod.Gadgets
{
    [Gadget(GADGET_NAME, true)]
    public class Miscellaneous : TienGadget<Miscellaneous>
    {
        public const string GADGET_NAME = "Miscellaneous";

        public override string ConfigVersion => "2.1.0";

        protected override string GadgetDescription =>
            "- Gadget RPG now increases TEC instead of DEX and FTH.\n" +
            "- Add more random names from franchises for character creation.";

        protected override void Initialize()
        {
            Logger.Log($"{GADGET_NAME} v{Info.Mod.Version}");
            Item<SCRP02>.I.Register();
        }
    }
}
