using System.Collections;
using TienContentMod.Gadgets;
using UnityEngine;

namespace TienContentMod.Scripts
{
    internal class HealWardTracker : MonoBehaviour
    {
        public Healward healWard;
        public Healward augur;

        public IEnumerator ReplaceWardWith(Healward other)
        {
            MoreCombatChips.Log("HealWardTracker.ReplaceWardWith: Called.");
            if (healWard != null)
            {
                MoreCombatChips.Log("HealWardTracker.ReplaceWardWith: healWard is not null. Executing Death.");
                healWard.GetComponent<Animation>().Play();
                yield return new WaitForSeconds(0.7f);
                Destroy(healWard.gameObject);
            }
            healWard = other;
            yield break;
        }

        public IEnumerator ReplaceAugurWith(Healward other)
        {
            MoreCombatChips.Log("HealWardTracker.ReplaceAugurWith: Called.");
            if (augur != null)
            {
                MoreCombatChips.Log("HealWardTracker.ReplaceAugurWith: augur is not null. Executing Death.");
                augur.GetComponent<Animation>().Play();
                yield return new WaitForSeconds(0.7f);
                Destroy(augur.gameObject);
            }
            augur = other;
            yield break;
        }
    }
}
