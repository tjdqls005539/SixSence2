using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RestaurantManager : MonoBehaviour
{
    public static RestaurantManager Instance;

    public List<Customer> customers = new List<Customer>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 예시: 모든 주문 음식 집계
    public Dictionary<string, int> GetFoodOrderCounts()
    {
        Dictionary<string, int> counts = new Dictionary<string, int>();

        foreach (Customer customer in customers)
        {
            Food ordered = customer.GetOrderedFood();
            if (ordered != null)
            {
                if (counts.ContainsKey(ordered.foodName)) counts[ordered.foodName]++;
                else counts.Add(ordered.foodName, 1);
            }
        }

        // 주문 많은 순서대로 정렬
        return new Dictionary<string, int>(counts.OrderByDescending(kv => kv.Value));
    }
}


