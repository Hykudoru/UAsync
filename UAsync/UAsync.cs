using System;
using System.Collections;
using UnityEngine;

namespace UAsync
{
    public static partial class Extensions
    {
        public static IEnumerator Async(this MonoBehaviour mb, Func<float, bool> conditionRun)
        {
            float elapsedTime = 0f;

            while (conditionRun(elapsedTime)) ;
            {
                elapsedTime += Time.unscaledDeltaTime;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;

                yield return null;
            }
        }

        public static IEnumerator Async(Func<bool> conditionMet)
        {
            while (!conditionMet())
            {
                yield return null;
            }
        }

        public static void Async(this MonoBehaviour mb, Func<bool> conditionMet)
        {
            mb.StartCoroutine(Async(conditionMet));
        }

        // INVOKE REPEATING

        static IEnumerator InvokeRepeatingAsync(Func<bool> conditionMet, float interval = 0f)
        {
            var delay = new WaitForSeconds(interval);

            while (!conditionMet())
            {
                yield return delay;
            }
        }

        public static void InvokeRepeating(this MonoBehaviour mb, Action action, float interval = 0f)
        {
            mb.StartCoroutine(InvokeRepeatingAsync(() => { action(); return false; }, interval));
        }

        public static void InvokeRepeating(this MonoBehaviour mb, Func<bool> conditionMet, float interval = 0f)
        {
            mb.StartCoroutine(InvokeRepeatingAsync(conditionMet, interval));
        }
    }
}