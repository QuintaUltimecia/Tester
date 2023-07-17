using UnityEngine;
using System.Collections;

public static class DoAnimation
{
    public static IEnumerator ScaleRoutine(this Transform target, Vector3 targetScale, float speed, System.Action onComplete = null)
    {
        while (target.localScale != targetScale)
        {
            target.localScale = Vector3.MoveTowards(target.localScale, targetScale, speed * Time.deltaTime);

            yield return new WaitForSeconds(Time.deltaTime);
        }

        onComplete?.Invoke();
    }

    public static IEnumerator MoveDown(this Transform target, Vector2 direction, float speed, System.Action onComplete = null)
    {
        while ((Vector2)target.position != direction)
        {
            target.position = Vector2.MoveTowards(target.position, direction, speed * Time.deltaTime);
            yield return new WaitForSeconds(Time.deltaTime);
        }

        onComplete?.Invoke();
    }
}
