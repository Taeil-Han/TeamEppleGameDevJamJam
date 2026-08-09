using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject tooltipPanel;
    [SerializeField] TMP_Text tooltipText;
    [SerializeField] string message = "Tooltip text here";

    void Start()
    {
        tooltipPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.text = message;
        tooltipPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipPanel.SetActive(false);
    }
}
