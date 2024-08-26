using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using TienContentMod.Gadgets;
using TienContentMod.ID;

namespace TienContentMod.Patches.BetterAugmentsPatches
{
    [HarmonyPatch(typeof(PlayerScript))]
    [HarmonyPatch("Update")]
    [HarmonyGadget(BetterAugments.GADGET_NAME)]
    public static class Patch_PlayerScript_Update
    {
        private static FieldInfo CurAugment
        {
            get => typeof(Menuu).GetField("curAugment", BindingFlags.Static | BindingFlags.Public);
        }

        private static FieldInfo Fspd
        {
            get => typeof(PlayerScript).GetField("fspd", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            DisableEyepodHatVanilla(p);
            DisableShmooHatVanilla(p);
            return p.Insns;
        }

        private static void DisableEyepodHatVanilla(TranspilerHelper.CILProcessor p)
        {
            var ilRefs = p.FindAllRefsByInsns(new List<CodeInstruction>
            {
                new CodeInstruction(OpCodes.Ldsfld, CurAugment),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)AugmentID.EyepodHat),
                new CodeInstruction(OpCodes.Bne_Un),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Stfld, Fspd)
            });
            if (ilRefs.Length == 0)
            {
                BetterAugments.Log("Patch_PlayerScript_Update: No reference point. (DisableEyepodHatVanilla)");
            }
            else
            {
                foreach (var ilRef in ilRefs)
                {
                    p.RemoveInsns(ilRef, 6);
                }
            }
        }

        private static void DisableShmooHatVanilla(TranspilerHelper.CILProcessor p)
        {
            var ilRefs = p.FindAllRefsByInsns(new List<CodeInstruction>
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld),
                new CodeInstruction(OpCodes.Brtrue),
                new CodeInstruction(OpCodes.Ldsfld, CurAugment),
                new CodeInstruction(OpCodes.Ldc_I4_6),
                new CodeInstruction(OpCodes.Beq)
            });
            if (ilRefs.Length == 0)
            {
                BetterAugments.Log("Patch_PlayerScript_Update: No reference point. (DisableShmooHatVanilla)");
            }
            else
            {
                foreach (var ilRef in ilRefs)
                {
                    p.RemoveInsns(ilRef + 3, 3);
                }
            }
        }
    }
}