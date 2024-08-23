using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using MoreCombatChips.ID;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MoreCombatChips.Patches
{
    [HarmonyPatch(typeof(EnemyScript))]
    [HarmonyPatch("DropLocal")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_EnemyScript_DropLocal
    {
        private static FieldInfo ItemQ
        {
            get => typeof(Item).GetField("q", BindingFlags.Public | BindingFlags.Instance);
        }

        private static FieldInfo Drops
        {
            get => typeof(EnemyScript).GetField("drops", BindingFlags.Public | BindingFlags.Instance);
        }

        private static MethodInfo DropEffectsMethod
        {
            get => typeof(Patch_EnemyScript_DropLocal).GetMethod(
                nameof(DropEffects),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRefs = p.FindAllRefsByInsns(new List<CodeInstruction>
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, Drops),
                null,
                new CodeInstruction(OpCodes.Ldelem_I4),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ble),
                new CodeInstruction(OpCodes.Ldloc_S),
                null,
                new CodeInstruction(OpCodes.Bge),
                new CodeInstruction(OpCodes.Ldc_I4_S, (byte)11),
                new CodeInstruction(OpCodes.Newarr, typeof(int)),
                new CodeInstruction(OpCodes.Dup),
                new CodeInstruction(OpCodes.Ldc_I4_0),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, Drops),
                null,
                new CodeInstruction(OpCodes.Ldelem_I4),
                new CodeInstruction(OpCodes.Stelem_I4),
                new CodeInstruction(OpCodes.Dup),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Ldloc_0),
                new CodeInstruction(OpCodes.Ldfld, ItemQ),
                new CodeInstruction(OpCodes.Stelem_I4)
            });
            if (ilRefs.Length == 0)
            {
                MoreCombatChips.Log("Patch_EnemyScript_DropLocal: Reference points not found.");
            }
            else
            {
                foreach (var matched in ilRefs)
                {
                    var ilRef = matched.GetRefByOffset(81);
                    p.InjectInsns(ilRef, new CodeInstruction[]
                    {
                        new CodeInstruction(OpCodes.Ldloca_S, 10),
                        new CodeInstruction(OpCodes.Call, DropEffectsMethod)
                    }, insert: true);
                }
            }
            return p.Insns;
        }

        private static void DropEffects(ref int[] array8)
        {
            if (Menuu.curAugment == AugmentID.ShmooHat && IsMonsterLoot(array8[0]))
            {
                array8[1] *= 3;
            }
        }

        private static bool IsMonsterLoot(int id)
        {
            return (ItemRegistry.GetTypeByID(id) & (ItemType.EMBLEM | ItemType.LOOT_MASK)) ==
                   (ItemType.LOOT | ItemType.MONSTER);
        }
    }
}
