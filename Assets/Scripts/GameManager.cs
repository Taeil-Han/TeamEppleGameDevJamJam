using UnityEngine;
using UnityEngine.SceneManagement;
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

    private double winAmount = 11345;

    

    void Start()
    {
        isGamePlaying = true;
        isGameLost = false;
        isGameWon = false;
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
            scoreManagerInst.money = 10;
        }

        isGamePlaying = false;
        Time.timeScale = 0f;

        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(new string[]
            {
                "Welcome to Sally\'s Seashell Shack on the Seashore. We are so stoked to have you slingin\' shells. But before we throw you to the fray, there are a couple things you should know.",
                "If you wanna get paid, you gotta hit quota. That number in the top left? That's your money tracker. Use it to buy new shells and abilities in the shop to unlock customers, but don\'t spend it all in one place!" ,
                "You\'ve gotta raise $11,345 before we let you go; it\'s all in the contract. Why is it that amount? I don\'t have a darn clue?! Have to read it upside down or something to understand that balderdash.",
                "You\'ll see our trusty supplier in the top right. To rack up money fast, make sure to upgrade your wares by opening the store with \"E\". If you find yourself running low, click these side buttons to stock up.",
                "Also, these sea creatures, although cute, are so prickly and clammy. Make sure to give them the right shell and never let them get close to your shop! Our trusty supplier will tell you the intel you need.",
                "Finally, use either the number keys or the scroll wheel to switch your current shell. Make sure you have the special shells bought from our supplier to obtain them",
                "Otherwise, move your sorry butt with \"A\" and \"D\", and make sure you never go under $0! Keep your aim steady! FIRE AWAY!"
            }, OnTutorialComplete);
        }
        else
        {
            OnTutorialComplete();
        }
    }
    void OnTutorialComplete()
    {
        Time.timeScale = 1f;
        isGamePlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerManager pm = playerInstance.GetComponent<PlayerManager>();
        CustomerSpawner cs = customerSpawnerInstance.GetComponent<CustomerSpawner>();
        /*if (Input.GetKey(KeyCode.Z))
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
        }*/
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
            SceneManager.LoadScene("LoseScene");
            //Debug.Log("YOU LOSEEEEE");
        }
        if (scoreManagerInst.money >= winAmount) 
        { 
            isGameWon = true;
            SceneManager.LoadScene("WinScene");
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
