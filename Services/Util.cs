using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using TienContentMod.Gadgets;
using UnityEngine;

namespace TienContentMod.Services
{
    public static class Util
    {
        public static bool RandomCheck(int chance, int probability = 100)
        {
            return UnityEngine.Random.Range(0, probability) < chance;
        }

        public static void PrintComponents(this GameObject gameObject, string gadget)
        {
            Component[] components = gameObject.GetComponents(typeof(Component));
            foreach (Component component in components)
            {
                TienContentMod.Log(gadget, component.ToString());
            }
        }

        public static void PrintInstructions(this IEnumerable<CodeInstruction> instructions, string gadget)
        {
            int i = 0;
            foreach (var instruction in instructions)
            {
                TienContentMod.Log(gadget, "\n---- Instruction " + ++i + " ----");
                TienContentMod.Log(gadget, "\nCode: " + instruction.opcode ?? "null");
                TienContentMod.Log(gadget, "\nOperand: " + instruction.operand ?? "null");
                TienContentMod.Log(gadget, "\nLabels:");
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
