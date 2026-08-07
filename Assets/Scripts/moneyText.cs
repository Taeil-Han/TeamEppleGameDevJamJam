using UnityEngine;
using TMPro;
public class moneyText : MonoBehaviour
{
    public TextMeshProUGUI moneyT;
    void Start()
    {
        UpdateMoney();
    }
    void Update()
    {
        UpdateMoney();
    }
    void UpdateMoney()
    {
        if (scoreManager.Instance == null)
        {
            moneyT.text = "Money: $0.00";
            return;
        }
        moneyT.text = "Money: $" + scoreManager.Instance.money.ToString("F2");
    }
}
