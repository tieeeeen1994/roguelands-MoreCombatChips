namespace MoreCombatChips.Services
{
    public static class Util
    {
        public static bool RandomCheck(int chance, int start = 0, int probability = 100)
        {
            return UnityEngine.Random.Range(start, probability) < chance;
        }
    }
}
