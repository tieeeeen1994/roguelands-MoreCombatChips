using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using TienContentMod.Gadgets;

namespace TienContentMod.Patches.DroidsReworkPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("CheckCheat")]
    [HarmonyGadget(DroidsRework.GADGET_NAME)]
    public static class Patch_GameScript_CheckCheat
    {
        private static FieldInfo CheatString
        {
            get => typeof(GameScript).GetField("cheatstring", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private static MethodInfo LogCheckRejectGold15Method
        {
            get => typeof(Patch_GameScript_CheckCheat).GetMethod(
                nameof(LogCheckRejectGold15),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        private static MethodInfo LogCheckPlatinumBadgeMethod
        {
            get => typeof(Patch_GameScript_CheckCheat).GetMethod(
                nameof(LogCheckPlatinumBadge),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRefStart = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, CheatString),
                new CodeInstruction(OpCodes.Ldstr, "2211221100"),
                new CodeInstruction(OpCodes.Call)
            });
            var ilRefEnd = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, CheatString),
                new CodeInstruction(OpCodes.Ldstr, "1111222200"),
                new CodeInstruction(OpCodes.Call)
            });
            if (ilRefStart == null)
            {
                DroidsRework.Log("Patch_GameScript_CheckCheat: Reference not found. (1)");
            }
            if (ilRefEnd == null)
            {
                DroidsRework.Log("Patch_GameScript_CheckCheat: Reference not found. (2)");
            }
            if (ilRefStart != null && ilRefEnd != null)
            {
                ilRefStart = ilRefStart.GetRefByOffset(5);
                int numberofInsns = ilRefEnd.Index - ilRefStart.Index;
                var ilRef = p.InjectInsns(ilRefStart, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldflda, CheatString),
                    new CodeInstruction(OpCodes.Call, LogCheckRejectGold15Method),
                });
                p.RemoveInsns(ilRef + 3, numberofInsns);
                p.InjectInsn(ilRefEnd + 5, new CodeInstruction(OpCodes.Call, LogCheckPlatinumBadgeMethod));
            }
            return p.Insns;
        }

        private static void LogCheckRejectGold15(ref string cheatstring)
        {
            DroidsRework.Log("Patch_GameScript_CheckCheat: Gold 15 Code rejected. Transpiler works.");
            cheatstring = string.Empty;
        }

        private static void LogCheckPlatinumBadge()
        {
            DroidsRework.Log("Patch_GameScript_CheckCheat: Platinum Badge Code detected. Transpiler works.");
        }
    }
}
