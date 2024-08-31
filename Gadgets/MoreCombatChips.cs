using GadgetCore.API;
using TienContentMod.CombatChips;

namespace TienContentMod.Gadgets
{
    [Gadget(GADGET_NAME, true)]
    public class MoreCombatChips : TienGadget<MoreCombatChips>
    {
        public const string GADGET_NAME = "More Combat Chips";

        public override string ConfigVersion => "2.1.1";

        protected override string GadgetDescription =>
            "- Adds new Combat Chips.\n" +
            "- Attacker Drone: Places a drone capable of attacking from above and below.\n" +
            "- Messy Mk. I: Throw Messy so it wreaks havoc to enemies near it. It needs energy to power it up.\n" +
            "- Rejuvenation Wave: Healing Ward heals +1 HP more per 100 FTH. " +
            "Angelic Augur heals +1 more HP per 50 FTH. This chip is not stackable.\n" +
            "- Blood Offering: Sacrifice 10 health to recover 50 mana.\n" +
            "- Passive chips that boosts stats now have XX version, which boosts respective stats by 24.\n" +
            "- Quadracopter is functionally fixed for multiplayer.\n" +
            "- Quadracopter now only costs 30 MP. (Configurable)" +
            "- Healing Wards and Angelic Augurs are now replaced when used again.";

        internal static bool QuadracopterCost = true;

        protected override void GadgetConfig()
        {
            QuadracopterCost = Config.ReadBool(
                "CopterCost", true,
                requiresRestart: true,
                comments: "Changes mana cost of Quadracopter to 30."
            );
        }

        protected override void Initialize()
        {
            Logger.Log($"{GADGET_NAME} v{Info.Mod.Version}");
            CombatChip<AttackerDroneChip>.I.Register();
            CombatChip<MessyMkIChip>.I.Register();
            CombatChip<VitalityXXChip>.I.Register();
            CombatChip<DexterityXXChip>.I.Register();
            CombatChip<StrengthXXChip>.I.Register();
            CombatChip<IntelligenceXXChip>.I.Register();
            CombatChip<TechXXChip>.I.Register();
            CombatChip<FaithXXChip>.I.Register();
            CombatChip<RejuvenationWaveChip>.I.Register();
            CombatChip<BloodOfferingChip>.I.Register();
        }
    }
}
