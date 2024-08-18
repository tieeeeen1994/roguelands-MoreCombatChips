using GadgetCore.API;

namespace MoreCombatChips.CombatChips
{
    public class IntelligenceXXChip : BaseChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Intelligence XX";

        public override string Description => "+24 Intelligence";

        public override EquipStats Stats => new EquipStats(0, 0, 0, 0, 24, 0);
    }
}
