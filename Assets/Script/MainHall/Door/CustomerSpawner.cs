using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform counterPoint;
    public Transform exitPoint;
    public float spawnInterval = 5f;

    IEnumerator Start()
    {
        while (true)
        {
            //  남은 시간이 20초 이하이면 더 이상 손님 생성 X
            if (TimeManager.Instance.GetRemainingTime() <= 20f)
                yield break;

            GameObject customerObj = Instantiate(customerPrefab, transform.position, Quaternion.identity);
            Customer customer = customerObj.GetComponent<Customer>();
            if (customer != null)
            {
                customer.counterPosition = counterPoint;
                customer.SetExit(exitPoint);
                customer.StartFindingChair();
            }

            //  방문 손님 1명 증가
            MoneyManager.Instance.AddVisitor();

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}


















