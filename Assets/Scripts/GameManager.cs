using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Player Management Vars
    [SerializeField] GameObject playerPrefab;
    private GameObject playerInstance;

    //General Vars
    private bool isGameLost = false;
    private bool isGamePaused = false;
    private bool isGameWon = false;

    //Enemy Management Vars
    [SerializeField] GameObject customerSpawner;
    private GameObject customerSpawnerInstance;
    int wavelvl = 1;

    //Shop Vars
    private double shopFunds = 0;

    private double winAmount = 1000000;

    

    void Start()
    {
        playerInstance = Instantiate(playerPrefab, new Vector3(0, -4, 0), Quaternion.identity);
        customerSpawnerInstance = Instantiate(customerSpawner, new Vector3(0, 0, 0), Quaternion.identity);
        if (scoreManager.Instance != null)
        {
            scoreManager.Instance.ResetScore();
            scoreManager.Instance.ResetMoney();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager pm = playerInstance.GetComponent<PlayerManager>();
        customerSpawner cs = customerSpawnerInstance.GetComponent<customerSpawner>();
        if (Input.GetKey(KeyCode.Z))
        { 
            if (pm != null && cs != null && wavelvl == 1)
            {
                UnlockLvl();
            }
        }
        if (Input.GetKey(KeyCode.X))
        {
            if (pm != null && cs != null && wavelvl == 2)
            {
                UnlockLvl();
            }
        }
    }

    public void UnlockLvl() 
    {
        PlayerManager pm = playerInstance.GetComponent<PlayerManager>();
        customerSpawner cs = customerSpawnerInstance.GetComponent<customerSpawner>();

        wavelvl++;
        pm.UnlockShell(wavelvl);
        cs.UnlockCustomer(wavelvl);
    }


}
