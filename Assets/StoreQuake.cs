using UnityEngine;
using System.Collections;

public class StoreQuake : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 originalPos;
    public static StoreQuake Instance;

    void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        originalPos = rectTransform.anchoredPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        float duration = 0.25f;
        float strength = 12f;
        float timer = 0f;
        while (timer < duration)
        {
            float x = Random.Range(-strength, strength);
            float y = Random.Range(-strength, strength);
            rectTransform.anchoredPosition = originalPos + new Vector2(x, y);
            timer += Time.unscaledDeltaTime;
            yield return null;
        }
        rectTransform.anchoredPosition = originalPos;
    }
}
