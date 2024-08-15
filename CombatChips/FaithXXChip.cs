using GadgetCore.API;
using MoreCombatChips.DataStructures;

namespace MoreCombatChips.CombatChips
{
    public class FaithXXChip : BaseChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Faith XX";

        public override string Description => "+24 Faith";

        public override EquipStats Stats => new EquipStats(0, 0, 0, 0, 0, 24);

        protected override void StoreExtraDetails(ref ModdedChip moddedChip)
        {
            base.StoreExtraDetails(ref moddedChip);
            ApplyMoreAdvancedChipData(ref moddedChip);
        }
    }
}