using GadgetCore.API;

namespace TienContentMod.Gadgets
{
    [Gadget(GADGET_NAME, true)]
    public class DroidsRework : TienGadget<DroidsRework>
    {
        public const string GADGET_NAME = "Droids Rework";

        public override string ConfigVersion => "2.2.0";

        protected override string GadgetDescription =>
            "- Droids are now affected by level scaling.\n" +
            "- Due to scaling, the vanilla base stats are too high, so they are adjusted.\n" +
            "- Gold 15 is now obtainable through Mystery Gift.\n" +
            "- The code to acquire Gold 15 is removed.";

        protected override void Initialize()
        {
            Logger.Log($"{GADGET_NAME} v{Info.Mod.Version}");
        }

        internal static bool TiersPlusGold = true;

        protected override void GadgetConfig()
        {
            TiersPlusGold = Config.ReadBool(
                "TiersPlusGold", true,
                requiresRestart: true,
                comments: "Removes Gold 15 from the Creation Machine added by Tiers+."
            );
        }
    }
}
