using GadgetCore.API;
using TienContentMod.Scripts;
using UnityEngine;

namespace TienContentMod.Projectiles
{
    internal class AttackerDroneProjectileResource : BaseProjectileResource
    {
        public void AddResource()
        {
            gameObject = Object.Instantiate(GadgetCoreAPI.GetSpecialProjectileResource("silversh"));
            AddProjectileBehavior();
            GadgetCoreAPI.AddCustomResource("TienContentMod/AttackerDroneProjectile", gameObject);
        }

        private void AddProjectileBehavior()
        {
            Object.Destroy(gameObject.GetComponent<Projectile>());
            gameObject.AddComponent<AttackerDroneProjectileScript>();
        }
    }
}
