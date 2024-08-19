using System.Collections;
using UnityEngine;

namespace MoreCombatChips.Scripts
{
    public class AttackerDroneScript : BaseTurretScript
    {
        private int damage;
        private int xDirection;
        private readonly int attackTimes = 30;
        private readonly float attackRate = 0.5f;
        private readonly float speed = 1.2f;

        public void Set(int damage, int xDirection)
        {
            // This only runs to the one who used the Chip to spawn the turret, so sync the sound effect and variables.
            GetComponent<NetworkView>().RPC("SetSound", RPCMode.All);
            GetComponent<NetworkView>().RPC("SyncFields", RPCMode.AllBuffered, damage, xDirection);
            GetComponent<NetworkView>().RPC("SetShoot", RPCMode.AllBuffered);
        }

        [RPC]
        private void SyncFields(int damage, int xDirection)
        {
            this.damage = damage;
            this.xDirection = xDirection;
            Vector3 oldScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(oldScale.x * xDirection, oldScale.y, oldScale.z);
        }

        private void Update()
        {
            transform.Translate(new Vector3(speed * xDirection, 0, 0) * Time.deltaTime);
        }

        private IEnumerator Shoot()
        {
            // This method is being synced through SetShoot, so no need to sync.
            for (int i = 0; i < attackTimes; i++)
            {
                yield return new WaitForSeconds(attackRate);
                SetProjectile();
            }
            yield return new WaitForSeconds(1f);
            Die();

            yield break;
        }

        private void SetProjectile()
        {
            GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/turret"), volume * .5f);
            GameObject projUp = (GameObject)Instantiate(Resources.Load("MoreCombatChips/AttackerDroneProjectile"),
                                                        transform.position, Quaternion.identity);
            projUp.GetComponent<AttackerDroneProjectileScript>().Set(damage, GameScript.MODS[10], xDirection, 1);
            GameObject projDown = (GameObject)Instantiate(Resources.Load("MoreCombatChips/AttackerDroneProjectile"),
                                                          transform.position, Quaternion.identity);
            projDown.GetComponent<AttackerDroneProjectileScript>().Set(damage, GameScript.MODS[10], xDirection, -1);
        }

        [RPC]
        private void SetShoot()
        {
            StartCoroutine(Shoot());
        }

        [RPC]
        private void SetSound()
        {
            GetComponent<AudioSource>().PlayOneShot((AudioClip)Resources.Load("Au/turret1"), volume * .5f);
        }
    }
}
