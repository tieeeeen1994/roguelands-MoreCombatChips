using GadgetCore.API;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace MoreCombatChips.Patches
{
    /// <summary>
    /// This will modify behavior in PlayerScript.Update.
    /// </summary>
    [HarmonyPatch(typeof(PlayerScript))]
    [HarmonyPatch("Update")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_PlayerScript_Update
    {
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            if (MoreCombatChips.EyepodHatChange)
            {
                var codes = instructions.ToList();
                var modifiedCodes = new List<CodeInstruction>();
                var augmentOperand = typeof(Menuu).GetField(
                    "curAugment",
                    BindingFlags.Static | BindingFlags.Public
                );
                var speedOperand = typeof(PlayerScript).GetField(
                    "fspd",
                    BindingFlags.Instance | BindingFlags.NonPublic
                );
                bool startIgnoring = false;
                int ignoreLimit = -1;
                for (int i = 0; i < codes.Count; i++)
                {
                    if (!startIgnoring && ignoreLimit == -1 &&
                        codes[i].opcode == OpCodes.Ldsfld && codes[i].operand == augmentOperand &&
                        codes[i + 1].opcode == OpCodes.Ldc_I4_S &&
                        codes[i + 2].opcode == OpCodes.Bne_Un &&
                        codes[i + 3].opcode == OpCodes.Ldarg_0 &&
                        codes[i + 4].opcode == OpCodes.Ldc_I4_0 &&
                        codes[i + 5].opcode == OpCodes.Stfld && codes[i + 5].operand == speedOperand)
                    {
                        startIgnoring = true;
                        ignoreLimit = i + 5;
                    }
                    if (startIgnoring)
                    {
                        if (i == ignoreLimit)
                        {
                            startIgnoring = false;
                        }
                    }
                    else
                    {
                        modifiedCodes.Add(codes[i]);
                    }
                }
                return modifiedCodes;
            }
            else
            {
                return instructions;
            }
        }
    }
}
