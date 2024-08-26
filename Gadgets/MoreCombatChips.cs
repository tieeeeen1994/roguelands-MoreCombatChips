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
            "- Quadracopter is functionally fixed for multiplayer.\n" +
            "- Quadracopter now only costs 30 MP. (Configurable)";

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
        }
    }
}