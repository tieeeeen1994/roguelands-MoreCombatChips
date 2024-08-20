﻿using GadgetCore.API;
using MoreCombatChips.Services;

namespace MoreCombatChips.CombatChips
{
    public class FaithXXChip : CombatChip
    {
        public override ChipType Type => ChipType.PASSIVE;

        public override string Name => "Faith XX";

        public override string Description => "+24 Faith";

        public override EquipStats Stats => StatService.EquipStats(FTH: 24);

        public override bool Advanced => true;
    }
}
