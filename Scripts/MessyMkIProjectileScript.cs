using System.Collections;
using UnityEngine;

namespace TienContentMod.Scripts
{
    public class MessyMkIProjectileScript : MonoBehaviour
    {
        private int damage;
        private float projRange;
        private Vector3 direction;
        private bool dying = false;
        private bool reborn = false;
        private bool dead = false;
        private float speed = 10f;
        private readonly float deathTimer = 0.3f;

        public void Set(int damage, int rangedMods, Vector3 direction)
        {
            this.damage = damage;
            projRange = rangedMods;
            this.direction = direction;
            StartCoroutine(Die());
        }

        public void Eye(int a)
        {
            if (!reborn)
            {
                damage += a;
                reborn = true;
                gameObject.transform.FindChild("atalanta").gameObject.SetActive(true);
                speed *= 1.2f;
            }
        }

        private void Update()
        {
            if (!dead)
            {
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            }
        }

        private IEnumerator Die()
        {
            if (!dying)
            {
                dying = true;
                yield return new WaitForSeconds(deathTimer + projRange * 0.01f);
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

        private void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.layer == 9 || c.gameObject.layer == 28)
            {
                float[] array = new float[] { damage, -100f }; // -100f is no knockback

                // We don't want to damage enemies locally, else there will be a life difference from server and client.
                // Always follow the server, and only damage in the server.
                if (Network.isServer)
                {
                    c.gameObject.SendMessage("TD", array);
                }

                Destroy(gameObject);
            }
        }
    }
}
