using GadgetCore.API;
using TienContentMod.Services;

namespace TienContentMod.CombatChips
{
    public class TechXXChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Tech XX";

        public override string Description => "+24 Tech";

        public override EquipStats Stats => StatService.EquipStats(TEC: 24);

        public override bool Advanced => true;
    }
}
