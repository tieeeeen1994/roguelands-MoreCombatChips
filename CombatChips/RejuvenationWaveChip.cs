using GadgetCore.API;

namespace MoreCombatChips.CombatChips
{
    public class RejuvenationWaveChip : BaseChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Rejuvenation Wave";

        public override string Description =>
            "Healing Ward: Heal +1 HP more per 100 FTH.\n" +
            "Angelic Augur: Heal +1 more HP per 50 FTH.\n" +
            "This effect is not stackable.";
    }
}
