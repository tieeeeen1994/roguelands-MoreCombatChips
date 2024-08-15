using UnityEngine;

namespace MoreCombatChips.Scripts
{
    public abstract class BaseTurretScript : BaseScript
    {
        protected virtual void Die()
        {
            Instantiate(Resources.Load("ex2"), transform.position, Quaternion.identity);
            Network.RemoveRPCs(GetComponent<NetworkView>().viewID);
            if (Network.isServer)
            {
                Network.Destroy(gameObject);
            }
        }
    }
}