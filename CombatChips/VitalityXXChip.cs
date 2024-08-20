using GadgetCore.API;
using MoreCombatChips.Services;

namespace MoreCombatChips.CombatChips
{
    public class VitalityXXChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Vitality XX";

        public override string Description => "+24 Vitality";

        public override EquipStats Stats => StatService.EquipStats(VIT: 24);

        public override bool Advanced => true;
    }
}
