using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public static CustomerSpawner Instance;
    [SerializeField] GameObject turtlePrefab;
    [SerializeField] GameObject otterPrefab;
    [SerializeField] GameObject sealPrefab;
    [SerializeField] float spawnTimeMIN = 0.5f;
    [SerializeField] float spawnTimeMAX = 2.5f;
    [SerializeField] float xMIN = -6f;
    [SerializeField] float xMAX = 6f;
    [SerializeField] float ySTART = 7f;
    private string[] orderGacha = { "Sundial" };

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
      StartCoroutine(SpawnCustomers());  
    }

    IEnumerator SpawnCustomers() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(spawnTimeMIN, spawnTimeMAX));
            Vector3 spawnPos = new Vector3(Random.Range(xMIN,xMAX), ySTART, 0f);
            string order = orderGacha[Random.Range(0, orderGacha.Length)];
            switch (order) 
            {
                case "Cone":
                    GameObject otterCustomer = Instantiate(otterPrefab, spawnPos, Quaternion.identity);
                    otterCustomer.GetComponent<Customer>().order = order;
                    break;
                case "Clam":
                    GameObject sealCustomer = Instantiate(sealPrefab, spawnPos, Quaternion.identity);
                    sealCustomer.GetComponent<Customer>().order = order;
                    break;

                default:
                    GameObject turtleCustomer = Instantiate(turtlePrefab, spawnPos, Quaternion.identity);
                    turtleCustomer.GetComponent<Customer>().order = order;
                    break;
            }
        }
    }

    public void UnlockCustomer(int lvl) 
    {
        if (lvl == 2)
        {
            orderGacha = new string[] { "Sundial", "Cone" };
        }
        if (lvl == 3)
        {
            orderGacha = new string[] { "Sundial", "Cone", "Clam" };
        }
    }
}
