using GadgetCore.API;
using GadgetCore.Util;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace MoreCombatChips.Patches
{
    [HarmonyPatch(typeof(PlayerScript))]
    [HarmonyPatch("Beam")]
    [HarmonyGadget("More Combat Chips")]
    public static class Patch_PlayerScript_Beam
    {
        private static FieldInfo CurAugment
        {
            get => typeof(Menuu).GetField("curAugment", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo HPField
        {
            get => typeof(GameScript).GetField("hp", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo MaxHPField
        {
            get => typeof(GameScript).GetField("maxhp", BindingFlags.Public | BindingFlags.Static);
        }

        private static FieldInfo GameScriptField
        {
            get => typeof(PlayerScript).GetField("gameScript", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        private static MethodInfo MechCityBeanieReworkMethod
        {
            get => typeof(Patch_PlayerScript_Beam).GetMethod(
                nameof(MechCityBeanieRework),
                BindingFlags.NonPublic | BindingFlags.Static
            );
        }

        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> insns, ILGenerator il)
        {
            var p = TranspilerHelper.CreateProcessor(insns, il);
            var ilRef = p.FindRefByInsns(new CodeInstruction[]
            {
                new CodeInstruction(OpCodes.Ldsfld, CurAugment),
                new CodeInstruction(OpCodes.Ldc_I4_S, 11),
                new CodeInstruction(OpCodes.Bne_Un),
                new CodeInstruction(OpCodes.Ldsfld, HPField),
                new CodeInstruction(OpCodes.Ldc_I4_5),
                new CodeInstruction(OpCodes.Add),
                new CodeInstruction(OpCodes.Stsfld, HPField),
                new CodeInstruction(OpCodes.Ldsfld, HPField),
                new CodeInstruction(OpCodes.Ldsfld, MaxHPField),
                new CodeInstruction(OpCodes.Ble),
                new CodeInstruction(OpCodes.Ldsfld, MaxHPField),
                new CodeInstruction(OpCodes.Stsfld, HPField)
            });
            if (ilRef == null)
            {
                MoreCombatChips.Log("Patch_PlayerScript_Beam: Cannot find reference.");
            }
            else
            {
                p.RemoveInsns(ilRef + 3, 9);
                p.InjectInsns(ilRef + 3, new CodeInstruction[]
                {
                    new CodeInstruction(OpCodes.Ldarg_0),
                    new CodeInstruction(OpCodes.Ldfld, GameScriptField),
                    new CodeInstruction(OpCodes.Call, MechCityBeanieReworkMethod)
                }, insert: true);
            }
            return p.Insns;
        }

        private static void MechCityBeanieRework(GameScript gameScript)
        {
            GameScript.hp = GameScript.maxhp;
            GameScript.mana = GameScript.maxmana;
            gameScript.UpdateMana();
        }
    }
}
