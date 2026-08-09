using UnityEngine;
using TMPro;
public class MoneyText : MonoBehaviour
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
        if (ScoreManager.Instance == null)
        {
            moneyT.text = "Money: $0.00";
            return;
        }
        moneyT.text = "Money: $" + ScoreManager.Instance.money.ToString("F2");
    }
}
