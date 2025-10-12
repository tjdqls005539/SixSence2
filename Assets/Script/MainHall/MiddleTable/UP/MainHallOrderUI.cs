using UnityEngine;
using UnityEngine.UI;

public class MainHallOrderUI : MonoBehaviour
{
    [Header("UI Components")]
    public Text foodNameText;
    public Image icon;

    private Customer customer;

    /// <summary>
    /// 주문 UI 초기화
    /// </summary>
    public void Init(Customer cust)
    {
        customer = cust;
        Food orderedFood = customer.GetOrderedFood();
        SetOrder(orderedFood);
    }

    /// <summary>
    /// Food 정보를 UI에 표시
    /// </summary>
    public void SetOrder(Food food)
    {
        if (food == null)
        {
            Debug.LogWarning("Food is null!");
            return;
        }

        if (foodNameText != null)
            foodNameText.text = food.foodName;

        if (icon != null)
        {
            if (food.icon != null)
                icon.sprite = food.icon;
            else
                icon.sprite = null; // 아이콘이 없으면 빈 이미지
        }
    }

    /// <summary>
    /// 주문 완료 버튼 클릭 시 호출
    /// </summary>
    public void OnOrderClicked()
    {
        if (customer == null)
            return;

        Food orderedFood = customer.GetOrderedFood();
        if (orderedFood != null)
        {
            OrderManager.Instance.AddOrder(orderedFood);
            customer.CompleteOrder();
        }
    }
}





