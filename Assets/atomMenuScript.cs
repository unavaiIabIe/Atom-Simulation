using System.Collections;
using UnityEngine;

public class atomMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject spawnAtomButton;
    [SerializeField] private GameObject menu;
    public bool isActive = false;
    public void activate()
    {
        if (!isActive)
        {
            RectTransform atomRectTransform = spawnAtomButton.GetComponent<RectTransform>();
            StartCoroutine(MoveUIElementCoroutine(atomRectTransform, new Vector2(-Screen.width * 0.2f, -508.2f), 0.05f));
            RectTransform menuRectTransform = menu.GetComponent<RectTransform>();
            StartCoroutine(MoveUIElementCoroutine(menuRectTransform, new Vector2(-838.48f, 0), 0.05f));
            isActive = true;
        }
        else
        {
            RectTransform atomRectTransform = spawnAtomButton.GetComponent<RectTransform>();
            StartCoroutine(MoveUIElementCoroutine(atomRectTransform, new Vector2(-790, -508.2f), 0.05f));
            RectTransform menuRectTransform = menu.GetComponent<RectTransform>();
            StartCoroutine(MoveUIElementCoroutine(menuRectTransform, new Vector2(-1239.72f, 0), 0.05f));
            isActive = false;
        }

        // Smooth transition
        IEnumerator MoveUIElementCoroutine(RectTransform uiElement, Vector2 targetPosition, float duration)
        {
            Vector2 startPosition = uiElement.anchoredPosition;
            float time = 0;

            while (time < duration)
            {
                time += Time.deltaTime;
                float t = time / duration;
                uiElement.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);
                yield return null;
            }

            uiElement.anchoredPosition = targetPosition;
        }

    }
}
