using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;

    void Start()
    {
        Instantiate(playerPrefab, new Vector3(0, -4, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
