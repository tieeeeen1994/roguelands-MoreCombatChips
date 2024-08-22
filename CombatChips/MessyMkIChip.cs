using GadgetCore.API;
using MoreCombatChips.Projectiles;
using MoreCombatChips.Scripts;
using UnityEngine;

namespace MoreCombatChips.CombatChips
{
    public class MessyMkIChip : CombatChip
    {
        public override int Damage => Mathf.Min(1, InstanceTracker.GameScript.GetFinalStat(3) / 10);

        public override ChipType Type => ChipType.ACTIVE;

        public override string Name => "Messy Mk. I";

        public override string Description => "Throw Messy so it wreaks havoc to enemies near it." +
                                              "It needs energy to power it up.";

        public override int Cost => 30;

        public override ChipInfo.ChipCostType CostType => ChipInfo.ChipCostType.ENERGY;

        public override bool Advanced => true;

        protected override void AddRequiredResources()
        {
            new MessyMkIResource().AddResource();
            new MessyMkIProjectileResource().AddResource();
        }

        protected override void Action(int slot)
        {
            Vector3 playerPos = InstanceTracker.PlayerScript.gameObject.transform.position;
            GameObject gameObject =
                (GameObject)Network.Instantiate(Resources.Load("MoreCombatChips/MessyMkI"),
                                                playerPos, Quaternion.identity, 0);
            gameObject.GetComponent<MessyMkIScript>().Set(Damage, ComputeDirection(playerPos));
        }

        private Vector3 ComputeDirection(Vector3 initialPosition)
        {
            // Z coordinate is also being updated.
            return (GadgetCoreAPI.GetCursorPos() - initialPosition).normalized;
        }
    }
}
