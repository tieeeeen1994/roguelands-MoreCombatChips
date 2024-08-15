using GadgetCore.API;
using MoreCombatChips.DataStructures;
using MoreCombatChips.Projectiles;
using MoreCombatChips.Scripts;
using UnityEngine;

namespace MoreCombatChips.CombatChips
{
    public class AttackerDroneChip : BaseChip
    {
        public override int Damage => InstanceTracker.GameScript.GetFinalStat(3);

        public override ChipType Type => ChipType.ACTIVE;

        public override string Name => "Attacker Drone";

        public override string Description => "Places a drone capable of attacking from above and below.";

        public override int Cost => 20;

        protected override void Action(int slot)
        {
            float cursorPosX = GadgetCoreAPI.GetCursorPos().x;
            Vector3 playerPos = InstanceTracker.PlayerScript.gameObject.transform.position;
            int xDirection = cursorPosX < playerPos.x ? -1 : 1;
            Vector3 spawnPos = playerPos + new Vector3(2f * xDirection, 0f, 0f);
            GameObject gameObject =
                (GameObject)Network.Instantiate(Resources.Load("MoreCombatChips/AttackerDrone"),
                                                spawnPos, Quaternion.identity, 0);
            gameObject.GetComponent<AttackerDroneScript>().Set(Damage, xDirection);
        }

        protected override void AddRequiredResources()
        {
            new AttackerDroneResource().AddResource();
            new AttackerDroneProjectileResource().AddResource();
        }

        protected override void StoreExtraDetails(ref ModdedChip moddedChip)
        {
            base.StoreExtraDetails(ref moddedChip);
            ApplyMoreAdvancedChipData(ref moddedChip);
        }
    }
}