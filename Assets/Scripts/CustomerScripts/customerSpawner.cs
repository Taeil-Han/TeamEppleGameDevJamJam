using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    [SerializeField] float spawnTimeMIN = 0.5f;
    [SerializeField] float spawnTimeMAX = 2.5f;
    [SerializeField] float xMIN = -6f;
    [SerializeField] float xMAX = 6f;
    [SerializeField] float ySTART = 7f;
    private string[] orderGacha = { "Sundial" };

    void Start()
    {
      StartCoroutine(SpawnCustomers());  
    }

    IEnumerator SpawnCustomers() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(spawnTimeMIN, spawnTimeMAX));
            Vector3 spawnPos = new Vector3(Random.Range(xMIN,xMAX), ySTART, 0f);
            GameObject newCustomer = Instantiate(customerPrefab, spawnPos, Quaternion.identity);
            newCustomer.GetComponent<Customer>().order = orderGacha[Random.Range(0, orderGacha.Length)];
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
