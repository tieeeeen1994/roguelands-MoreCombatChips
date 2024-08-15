using GadgetCore.API;
using MoreCombatChips.DataStructures;

namespace MoreCombatChips.CombatChips
{
    public class StrengthXXChip : BaseChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Strength XX";

        public override string Description => "+24 Strength";

        public override EquipStats Stats => new EquipStats(0, 24, 0, 0, 0, 0);

        protected override void StoreExtraDetails(ref ModdedChip moddedChip)
        {
            base.StoreExtraDetails(ref moddedChip);
            ApplyMoreAdvancedChipData(ref moddedChip);
        }
    }
}