using GadgetCore.API;
using TienContentMod.Scripts;
using UnityEngine;

namespace TienContentMod.Projectiles
{
    public class AttackerDroneResource : BaseProjectileResource
    {
        public void AddResource()
        {
            gameObject = Object.Instantiate(GadgetCoreAPI.GetProjectileResource("turret"));
            AddDroneBehavior();
            UpdateTexture();
            GadgetCoreAPI.AddCustomResource("TienContentMod/AttackerDrone", gameObject);
        }

        private void AddDroneBehavior()
        {
            Object.Destroy(gameObject.GetComponent<TurretScript>());
            gameObject.AddComponent<AttackerDroneScript>();
        }

        private void UpdateTexture()
        {
            foreach (MeshRenderer child in gameObject.transform.GetComponentsInChildren<MeshRenderer>())
            {
                switch (child.name)
                {
                    case "Plane_001":
                    case "eye":
                        Object.Destroy(child.gameObject);
                        break;

                    case "Plane":
                        child.material.SetTexture("_MainTex", GadgetCoreAPI.LoadTexture2D("Projectiles/B2"));
                        break;
                }
            }
        }
    }
}