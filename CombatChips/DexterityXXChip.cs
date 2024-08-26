using GadgetCore.API;
using TienContentMod.Services;

namespace TienContentMod.CombatChips
{
    public class DexterityXXChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Dexterity XX";

        public override string Description => "+24 Dexterity";

        public override EquipStats Stats => StatService.EquipStats(DEX: 24);

        public override bool Advanced => true;
    }
}