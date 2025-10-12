using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    private List<Food> orders = new List<Food>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // 주문 추가 (중복 허용)
    public void AddOrder(Food food)
    {
        if (food != null)
            orders.Add(food);
    }

    // 현재 주문 리스트 반환
    public List<Food> GetOrders()
    {
        return new List<Food>(orders);
    }

    // 주문 초기화
    public void ClearOrders()
    {
        orders.Clear();
    }

    //  메뉴별 주문 횟수 집계 (많이 주문된 순으로 정렬)
    public Dictionary<string, int> GetFoodOrderCounts()
    {
        return orders.GroupBy(f => f.foodName)
                     .OrderByDescending(g => g.Count())
                     .ToDictionary(g => g.Key, g => g.Count());
    }
}





