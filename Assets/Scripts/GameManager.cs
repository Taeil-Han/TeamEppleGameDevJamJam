using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Player Management Vars
    [SerializeField] GameObject playerPrefab;
    private int[] numOfShells = new int[5] { 10, 0, 0, 0, 0 };

    //General Vars
    private bool isGameLost = false;
    private bool isGamePaused = false;
    private bool isGameWon = false;

    //Enemy Management Vars
    private int waveLevel = 1;

    //Shop Vars
    private double shopFunds = 0;

    private double winAmount = 1000000;

    

    void Start()
    {
        Instantiate(playerPrefab, new Vector3(0, -4, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
