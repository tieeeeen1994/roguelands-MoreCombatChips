using GadgetCore.API;

namespace MoreCombatChips.CombatChips
{
    public class TechXXChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Tech XX";

        public override string Description => "+24 Tech";

        public override EquipStats Stats => new EquipStats(0, 0, 0, 24, 0, 0);
    }
}
