using GadgetCore.API;
using TienContentMod.ID;

namespace TienContentMod.Gadgets
{
    [Gadget(GADGET_NAME, true)]
    public class BetterAugments : TienGadget<BetterAugments>
    {
        public const string GADGET_NAME = "Better Augments";

        public override string ConfigVersion => "2.1.0";

        protected override string GadgetDescription =>
            "- Mech City Beanie is buffed to fully recover health and mana when beaming.\n" +
            "- Adds new effects to augments that had no effect.\n" +
            "- Reworks negative augments to have a positive effect.";

        protected override void Initialize()
        {
            Logger.Log($"{GADGET_NAME} v{Info.Mod.Version}");
            GadgetCoreAPI.RegisterStatModifier(AugmentModifiers, StatModifierType.FinalExpMult);
        }

        private EquipStatsDouble AugmentModifiers(Item _)
        {
            var equipStats = EquipStatsDouble.ONE;
            switch (Menuu.curAugment)
            {
                case AugmentID.EyepodHat:
                    equipStats = new EquipStatsDouble(0.5);
                    break;
            }
            return equipStats;
        }
    }
}
