using UnityEngine;
using TMPro;
public class StageText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lvlT;
    void Start()
    {
        UpdateLvl();
    }
    void Update()
    {
        UpdateLvl();
    }
    void UpdateLvl()
    {
        if (ShopMenu.Instance != null)
        {
            lvlT.text = "Stage Lvl: " + ShopMenu.Instance.lvl.ToString();
        }
    }
}
