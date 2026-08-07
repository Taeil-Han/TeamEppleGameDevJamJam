using UnityEngine;
using System.Collections;

public class customerSpawner : MonoBehaviour
{
    public GameObject customer;
    public float spawnTimeMIN = 0.5f;
    public float spawnTimeMAX = 2.5f;
    public float xMIN = -6f;
    public float xMAX = 6f;
    public float ySTART = 7f;

    void Start()
    {
      StartCoroutine(SpawnCustomers());  
    }

    IEnumerator SpawnCustomers() {
        while(true) {
            yield return new WaitForSeconds(Random.Range(spawnTimeMIN, spawnTimeMAX));
            Vector3 spawnPos = new Vector3(Random.Range(xMIN,xMAX), ySTART, 0f);
            Instantiate(customer, spawnPos, Quaternion.identity);
        }
    }
}
