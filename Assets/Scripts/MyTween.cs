using System;
using System.Collections;
using UnityEngine;

public class MyTween : MonoBehaviour
{
    public IEnumerator AnimatedScaleCR(Vector3 targetScale, float duration)
    {
        Vector3 initScale = transform.localScale;
        float timer = 0;
        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(initScale, targetScale, timer / duration);
            yield return null;
        }
    }
}