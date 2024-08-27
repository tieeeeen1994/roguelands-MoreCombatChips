using GadgetCore.API;
using HarmonyLib;
using TienContentMod.Gadgets;
using TienContentMod.ID;
using UnityEngine;

namespace TienContentMod.Patches.DroidsReworkPatches
{
    [HarmonyPatch(typeof(GameScript))]
    [HarmonyPatch("MysteryBox")]
    [HarmonyGadget(DroidsRework.GADGET_NAME)]
    public static class Patch_GameScript_MysteryBox
    {
        [HarmonyPrefix]
        public static bool Prefix(Vector3 pp, GameScript __instance)
        {
            int random = UnityEngine.Random.Range(0, 100);
            if (random == 0)
            {
                __instance.GetComponent<AudioSource>().PlayOneShot(
                    (AudioClip)Resources.Load("Au/glitter4"),
                    Menuu.soundLevel / 10f
                );
                DroidsRework.Log("Patch_GameScript_MysteryBox: Got Gold 15!");
                CreateEquippableItem(__instance, pp, ItemID.GOLD15);
                return false;
            }
            return true;
        }

        private static void CreateEquippableItem(GameScript instance, Vector3 pp, int id)
        {
            var details = new int[11];
            details[0] = id;
            details[1] = 1;
            details[3] = instance.GetRandomTier();
            var item = (GameObject)Network.Instantiate(Resources.Load("i"), pp, Quaternion.identity, 0);
            item.GetComponent<NetworkView>().RPC("Init", RPCMode.AllBuffered, new object[] { details });
        }
    }
}
