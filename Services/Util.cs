using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace MoreCombatChips.Services
{
    public static class Util
    {
        public static bool RandomCheck(int chance, int start = 0, int probability = 100)
        {
            return UnityEngine.Random.Range(start, probability) < chance;
        }

        public static void PrintInstructions(IEnumerable<CodeInstruction> instructions)
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
