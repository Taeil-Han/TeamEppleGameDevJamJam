using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    //Player Management Vars
    [SerializeField] GameObject playerPrefab;
    private GameObject playerInstance;
    private PlayerManager playerManager;

    //General Vars
    private bool isGameLost = false;
    private bool isGameWon = false;
    private bool isGamePlaying;
    private bool isGamePaused = false;

    //Score and Money
    [SerializeField] GameObject playerUIPrefab;
    private PlayerUI playerUI;
    private ScoreManager scoreManagerInst;
    

    //Enemy Management Vars
    [SerializeField] GameObject customerSpawner;
    private GameObject customerSpawnerInstance;
    public int wavelvl = 1;

    //Shop Vars
    private double shopFunds = 0;

    private double winAmount = 1000000;

    

    void Start()
    {
        isGamePlaying = true;
        playerInstance = Instantiate(playerPrefab, new Vector3(0, -4, 0), Quaternion.identity);
        playerManager = playerInstance.GetComponent<PlayerManager>();
        customerSpawnerInstance = Instantiate(customerSpawner, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject uiInstance = Instantiate(playerUIPrefab);
        playerUI = uiInstance.GetComponent<PlayerUI>();
        playerUI.Init(playerManager, this);
        scoreManagerInst = ScoreManager.Instance;


        if (scoreManagerInst != null)
        {
            scoreManagerInst.ResetScore();
            scoreManagerInst.ResetMoney();
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager pm = playerInstance.GetComponent<PlayerManager>();
        CustomerSpawner cs = customerSpawnerInstance.GetComponent<CustomerSpawner>();
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
        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        if (scoreManagerInst == null) 
        {
            Debug.Log("ScoreManager is null");
            return;
        }
        if (scoreManagerInst.money < 0) 
        { 
            isGameLost = true;
            //TODO: ADD GAMING LOSING LOGIC
            Debug.Log("YOU LOSEEEEE");
        }
        if (scoreManagerInst.money >= winAmount) 
        { 
            isGameWon = true;
            //TODO: ADD GAMING WINNING LOGIC
        }
    }

    public void UnlockLvl() 
    {
        PlayerManager pm = playerInstance.GetComponent<PlayerManager>();
        CustomerSpawner cs = customerSpawnerInstance.GetComponent<CustomerSpawner>();

        wavelvl++;
        pm.UnlockShell(wavelvl);
        cs.UnlockCustomer(wavelvl);
    }


}
