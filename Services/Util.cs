using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using TienContentMod.Gadgets;

namespace TienContentMod.Services
{
    public static class Util
    {
        public static bool RandomCheck(int chance, int probability = 100)
        {
            return UnityEngine.Random.Range(0, probability) < chance;
        }

        public static void PrintInstructions(this IEnumerable<CodeInstruction> instructions)
        {
            int i = 0;
            foreach (var instruction in instructions)
            {
                MoreCombatChips.Log("\n---- Instruction " + ++i + " ----");
                MoreCombatChips.Log("\nCode: " + instruction.opcode ?? "null");
                MoreCombatChips.Log("\nOperand: " + instruction.operand ?? "null");
                MoreCombatChips.Log("\nLabels:");
                PrintLabels(instruction.labels);
            }
        }

        private static void PrintLabels(List<Label> labels)
        {
            foreach (var label in labels)
            {
                MoreCombatChips.Log("\n~ " + label ?? "null");
            }
        }
    }
}
