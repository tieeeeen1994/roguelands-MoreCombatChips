using System;

namespace MoreCombatChips.Exceptions
{
    internal class GameScriptCombatChipsNotFoundException : Exception
    {
        public GameScriptCombatChipsNotFoundException() : base("GameScript.combatChips not found.") { }
    }
}
