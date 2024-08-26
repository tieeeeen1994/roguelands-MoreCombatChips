using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TienContentMod.Gadgets;
using UnityEngine;

namespace TienContentMod.Projectiles
{
    public abstract class BaseProjectileResource
    {
        public GameObject gameObject;

        [MethodImpl(MethodImplOptions.NoInlining)]
        protected void LogComponents(Type type, Func<Type, Component[]> func,
                                     string message = "Loop through components")
        {
            string methodName = new StackTrace().GetFrame(1).GetMethod().Name;
            MoreCombatChips.Log($"{GetType().Name}.{methodName}: {message}.");
            foreach (var child in func(type))
            {
                MoreCombatChips.Log($"- {child.GetType().Name} : {child.name}");
            }
        }
    }
}