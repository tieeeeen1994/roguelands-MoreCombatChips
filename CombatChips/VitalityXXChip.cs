using GadgetCore.API;

namespace MoreCombatChips.CombatChips
{
    public class VitalityXXChip : BaseChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Vitality XX";

        public override string Description => "+24 Vitality";

        public override EquipStats Stats => new EquipStats(24, 0, 0, 0, 0, 0);
    }
}
