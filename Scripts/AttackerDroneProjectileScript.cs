using System.Collections;
using UnityEngine;

namespace MoreCombatChips.Scripts
{
    public class AttackerDroneProjectileScript : MonoBehaviour
    {
        private int damage;
        private float projRange;
        private Vector3 newPosition;
        private int xDirection;
        private int yDirection;
        private bool dying = false;
        private bool reborn = false;
        private bool dead = false;
        private float xCurrentSpeed = 10f;
        private float yCurrentSpeed = 0f;
        private readonly float deathTimer = 1.5f;
        private readonly float xRate = -0.05f;
        private readonly float yRate = 0.3f;
        private readonly float minXSpeed = .75f;
        private readonly float maxYSpeed = 40f;

        public void Set(int damage, int rangedMods, int xDirection, int yDirection)
        {
            // The projectiles produced by the turret does not need to be synced
            // because the turret script that spawned it should already have synced the required passed parameters.
            this.damage = damage;
            projRange = rangedMods;
            this.xDirection = xDirection;
            this.yDirection = yDirection;
            xCurrentSpeed *= xDirection;
            yCurrentSpeed *= yDirection;
            UpdatePosition();
            StartCoroutine(Die());
        }

        private void Update()
        {
            if (!dead)
            {
                xCurrentSpeed += xRate * xDirection;
                yCurrentSpeed += yRate * yDirection;
                UpdatePosition();
                ClampSpeeds();
                transform.Translate(newPosition);
            }
        }

        private IEnumerator Die()
        {
            if (!dying)
            {
                dying = true;
                yield return new WaitForSeconds(deathTimer + projRange * 0.025f);
                if (!reborn)
                {
                    dead = true;
                    Destroy(gameObject);
                }
                else
                {
                    reborn = false;
                    dying = false;
                    StartCoroutine(Die());
                }
            }
            yield break;
        }

        private void UpdatePosition()
        {
            newPosition = new Vector3(xCurrentSpeed, yCurrentSpeed, 0) * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.layer == 9 || c.gameObject.layer == 28)
            {
                float[] array = new float[] { damage, -100f }; // -100f is no knockback

                // We don't want to damage enemies locally, or else there will be a life difference from server and client.
                // Always follow the server, and only damage in the server.
                if (Network.isServer)
                {
                    c.gameObject.SendMessage("TD", array);
                }
            }
        }

        private void ClampSpeeds()
        {
            if (xDirection > 0)
            {
                xCurrentSpeed = Mathf.Max(xCurrentSpeed, minXSpeed);
            }
            else
            {
                xCurrentSpeed = Mathf.Min(xCurrentSpeed, -minXSpeed);
            }
            if (yDirection > 0)
            {
                yCurrentSpeed = Mathf.Min(yCurrentSpeed, maxYSpeed);
            }
            else
            {
                yCurrentSpeed = Mathf.Max(yCurrentSpeed, -maxYSpeed);
            }
        }
    }
}