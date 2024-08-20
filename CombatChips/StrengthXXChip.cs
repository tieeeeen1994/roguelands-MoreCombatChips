using GadgetCore.API;
using MoreCombatChips.Services;

namespace MoreCombatChips.CombatChips
{
    public class StrengthXXChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Strength XX";

        public override string Description => "+24 Strength";

        public override EquipStats Stats => StatService.EquipStats(STR: 24);

        public override bool Advanced => true;
    }
}
