using GadgetCore.API;

namespace MoreCombatChips.CombatChips
{
    public class DexterityXXChip : BaseChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Dexterity XX";

        public override string Description => "+24 Dexterity";

        public override EquipStats Stats => new EquipStats(0, 0, 24, 0, 0, 0);
    }
}
