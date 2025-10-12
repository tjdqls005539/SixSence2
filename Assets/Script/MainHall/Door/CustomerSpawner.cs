// CustomerSpawner.cs
using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform counterPoint; // 씬의 카운터 오브젝트
    public Transform exitPoint;    // 출구 오브젝트
    public float spawnInterval = 5f;

    void Start()
    {
        if (customerPrefab == null || counterPoint == null || exitPoint == null)
        {
            Debug.LogError("[CustomerSpawner] 필수 변수가 설정되지 않았습니다.");
            return;
        }

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            GameObject customerObj = Instantiate(customerPrefab, transform.position, Quaternion.identity);
            Customer customer = customerObj.GetComponent<Customer>();
            if (customer != null)
            {
                customer.counterPosition = counterPoint;
                customer.SetExit(exitPoint);
                customer.StartFindingChair();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}















