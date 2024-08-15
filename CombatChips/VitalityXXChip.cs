using GadgetCore.API;
using MoreCombatChips.DataStructures;

namespace MoreCombatChips.CombatChips
{
    public class VitalityXXChip : BaseChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Vitality XX";

        public override string Description => "+24 Vitality";

        public override EquipStats Stats => new EquipStats(24, 0, 0, 0, 0, 0);

        protected override void StoreExtraDetails(ref ModdedChip moddedChip)
        {
            base.StoreExtraDetails(ref moddedChip);
            ApplyMoreAdvancedChipData(ref moddedChip);
        }
    }
}