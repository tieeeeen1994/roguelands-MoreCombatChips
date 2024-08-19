using GadgetCore.API;

namespace MoreCombatChips.CombatChips
{
    public class StrengthXXChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Strength XX";

        public override string Description => "+24 Strength";

        public override EquipStats Stats => new EquipStats(0, 24, 0, 0, 0, 0);
    }
}
