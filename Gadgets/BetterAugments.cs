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
            "- Mech City Beanie is buffed to fully recover health and mana when beaming.\n" +
            "- Rebellion Headpiece: Max HP is now 75. Gain 2 DEX per level up.\n" +
            "- Creator Mask: 25% chance to gain 1 MAG or 75% chance to gain 1 FTH.\n" +
            "- Eyepod Hat: On level up, stats gained are double, but equipment is only 50% effective.\n" +
            "- Gas Mask: When using a Combat Chip with insufficent mana, consume Stamina instead.\n" +
            "- Glibglob Hat: Gain 50% more EXP from enemies.\n" +
            "- Chamcham Hat: Gain 3% more chance into crafting higher tiered gear.\n" +
            "- Shmoo Hat: Monster loot drops are tripled.\n" +
            "- Shroom Hat: Combat Chips provide twice the effect when enhancing stats.\n" +
            "- Slime Hat: Gain double EXP from the Gear Chalice.";

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
