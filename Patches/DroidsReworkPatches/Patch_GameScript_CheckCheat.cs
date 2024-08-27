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
            if (ilRefStart == null || ilRefEnd == null)
            {
                DroidsRework.Log("Patch_GameScript_CheckCheat: Reference not found.");
            }
            else
            {
                p.RemoveInsns(ilRefStart, ilRefEnd);
            }
            return p.Insns;
        }
    }
}
