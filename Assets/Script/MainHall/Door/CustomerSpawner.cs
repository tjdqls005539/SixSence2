using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    [Header("손님 생성")]
    public GameObject customerPrefab;
    public Transform parentTransform;   // 생성될 손님 부모
    public float spawnInterval = 5f;

    [Header("카운터/출구 설정")]
    public Transform counterPoint;
    public Transform exitPoint;

    private bool isSpawning = false;

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnCustomers());
        }
    }

    private IEnumerator SpawnCustomers()
    {
        while (TimeManager.Instance != null && TimeManager.Instance.IsGameActive())
        {
            if (customerPrefab != null)
            {
                // 손님 생성
                GameObject customerObj = Instantiate(customerPrefab, transform.position, Quaternion.identity);

                // 부모 설정
                if (parentTransform != null)
                    customerObj.transform.SetParent(parentTransform);

                // 스크립트 연결
                Customer customer = customerObj.GetComponent<Customer>();
                if (customer != null)
                {
                    customer.moneyManager = MoneyManager.Instance;       // MoneyManager 자동 연결
                    customer.counterPosition = counterPoint;            // 카운터 위치
                    customer.exitPositionTransform = exitPoint;         // 출구 위치
                    customer.StartFindingChair();                        // 의자 찾기 시작
                }

                MoneyManager.Instance?.AddVisitor(); // 방문자 기록
            }

            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
        Debug.Log("손님 스폰 종료");
    }
}














































