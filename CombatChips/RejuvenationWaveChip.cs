using GadgetCore.API;

namespace TienContentMod.CombatChips
{
    public class RejuvenationWaveChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Rejuvenation Wave";

        public override string Description =>
            "Healing Ward: Heal +1 HP more per 100 FTH.\n" +
            "Angelic Augur: Heal +1 more HP per 50 FTH.\n" +
            "This chip is not stackable.";

        public override bool Advanced => true;
    }
}