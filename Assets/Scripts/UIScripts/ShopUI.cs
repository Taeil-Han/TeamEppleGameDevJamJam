using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] RectTransform buttonsContainer; // the object holding your 3 buttons
    [SerializeField] float hiddenX = 200f;   // offscreen/hidden position (relative offset)
    [SerializeField] float shownX = 0f;      // visible position
    [SerializeField] float slideSpeed = 10f;

    private float targetX;

    void Start()
    {
        targetX = hiddenX;
        SetX(hiddenX);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hover ENTER detected");
        targetX = shownX;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Hover EXIT detected");
        targetX = hiddenX;
    }

    void Update()
    {
        float currentX = buttonsContainer.anchoredPosition.x;
        float newX = Mathf.Lerp(currentX, targetX, Time.deltaTime * slideSpeed);
        SetX(newX);
        Debug.Log("Current: " + currentX + " | Target: " + targetX + " | New: " + newX);
    }

    void SetX(float x)
    {
        Vector2 pos = buttonsContainer.anchoredPosition;
        pos.x = x;
        buttonsContainer.anchoredPosition = pos;
    }
}
