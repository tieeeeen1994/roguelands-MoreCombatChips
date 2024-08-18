using System;

namespace MoreCombatChips.Exceptions
{
    internal class ModdedChipNotFoundException : Exception
    {
        public ModdedChipNotFoundException(string name) : base($"{name} chip not found.") { }
    }
}
