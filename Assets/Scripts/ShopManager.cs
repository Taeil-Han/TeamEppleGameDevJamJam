using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private bool isShopOpen;

    private bool[] shellUnlocked = new bool[3]; //Increase to 5 for larger scope
    private int[] shellCosts = new int[3];
    private double fund = 0;

    // Update is called once per frame
    void Update()
    {
        if (shellCosts[1] > fund) 
        { 
            //Grey-out button
        }
        if (shellCosts[2] > fund)
        {
            //Grey-out button
        }
    }

    public void buyShell2()
    {
        if (shellCosts[1] > fund) 
        { 
            //Unlock Shell Lvl2
        }
    }

    public void buyShell3()
    {
        if (shellCosts[2] > fund)
        {
            //Unlock Shell Lvl3
        }
    }
}
