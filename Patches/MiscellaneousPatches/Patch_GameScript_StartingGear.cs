using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;
using TienContentMod.Items;
using TienContentMod.Services;

namespace TienContentMod.Patches.MiscellaneousPatches
{
    [Harmony]
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("StartingGear")]
    [HarmonyGadget(Miscellaneous.GADGET_NAME)]
    public static class Patch_GameScript_StartingGear
    {
        [HarmonyPostfix]
        public static bool Prefix(ref Item[] ___inventory)
        {
            ___inventory[InventorySlotID.Droid1st] = ItemService.NewItem(Item<SCRP02>.ID);
            ___inventory[InventorySlotID.Weapon] = ItemService.NewItem(Item<IronSword>.ID);
            // var stats = InitializeStats();
            // int maxStat = stats.Max();
            // var maxStatIds = stats.Select((stat, index) => new { stat, index }).Where(x => x.stat == maxStat)
            //                       .Select(x => x.index).ToArray();
            // int chosenStatId = maxStatIds[UnityEngine.Random.Range(0, maxStatIds.Length)];
            // ___inventory[InventorySlotID.Weapon] = ChooseWeapon(chosenStatId);
            return false;
        }

        // private static List<int> InitializeStats()
        // {
        //     var stats = StatService.ListStats();
        //     for (int i = 0; i < Menuu.raceStat.Length; i++)
        //     {
        //         stats[i] += Menuu.raceStat[i];
        //         if (Menuu.curTrait0 == i || Menuu.curTrait1 == i)
        //         {
        //             stats[i] += 1;
        //         }
        //     }
        //     return stats;
        // }

        // private static Item ChooseWeapon(int statId)
        // {
        //     int? weaponId = null;
        //     switch (statId)
        //     {
        //         case StatID.VIT:
        //             weaponId = ItemID.Aetherlance;
        //             break;
        //         case StatID.STR:
        //             weaponId = ItemID.Aetherblade;
        //             break;
        //         case StatID.TEC:
        //             weaponId = ItemID.Aethercannon;
        //             break;
        //         case StatID.DEX:
        //             weaponId = ItemID.AethergunMkIV;
        //             break;
        //         case StatID.FTH:
        //             weaponId = ItemID.Aetherstaff;
        //             break;
        //         case StatID.MAG:
        //             weaponId = ItemID.MageGauntlet;
        //             break;
        //     }
        //     if (weaponId.HasValue)
        //     {
        //         return ItemService.NewItem(weaponId.Value);
        //     }
        //     else
        //     {
        //         string message = "No weapon ID chosen.";
        //         Miscellaneous.Error(message);
        //         throw new Exception(message);
        //     }
        // }
    }
}