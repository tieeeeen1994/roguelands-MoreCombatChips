using GadgetCore.API;
using TienContentMod.Scripts;
using UnityEngine;

namespace TienContentMod.Projectiles
{
    public class MessyMkIResource : BaseProjectileResource
    {
        public void AddResource()
        {
            gameObject = Object.Instantiate(GadgetCoreAPI.GetProjectileResource("turret"));
            AddDroneBehavior();
            UpdateTexture();
            GadgetCoreAPI.AddCustomResource("TienContentMod/MessyMkI", gameObject);
        }

        private void AddDroneBehavior()
        {
            Object.Destroy(gameObject.GetComponent<TurretScript>());
            gameObject.AddComponent<MessyMkIScript>();
        }

        private void UpdateTexture()
        {
            foreach (MeshRenderer child in gameObject.transform.GetComponentsInChildren<MeshRenderer>())
            {
                switch (child.name)
                {
                    case "Plane":
                    case "Plane_001":
                        Object.Destroy(child.gameObject);
                        break;

                    case "eye":
                        CreateClone(child.gameObject);
                        child.material.SetTexture("_MainTex", GadgetCoreAPI.LoadTexture2D("Projectiles/MessyOneGray"));
                        child.sortingOrder = 1;
                        break;
                }
            }
        }

        private void CreateClone(GameObject gameObjectToClone)
        {
            GameObject clone = Object.Instantiate(
                gameObjectToClone,
                gameObjectToClone.transform.position,
                gameObjectToClone.transform.rotation,
                gameObjectToClone.transform.parent
            );
            MeshRenderer cloneMesh = clone.GetComponent<MeshRenderer>();
            cloneMesh.material.SetTexture("_MainTex", GadgetCoreAPI.LoadTexture2D("Projectiles/MessyOneBrown"));
            cloneMesh.sortingOrder = 0;
        }
    }
}