using UnityEngine;
using UnityEngine.UI;

public class MainHallOrderUI : MonoBehaviour
{
    public Text foodNameText;
    public Image icon;

    private Customer customer; // Customer 참조만 함, 정의 X

    public void Init(Customer cust)
    {
        customer = cust;
        SetOrder(customer.GetOrderedFood());
    }

    public void SetOrder(Food food)
    {
        if (foodNameText != null) foodNameText.text = food.foodName;
        if (icon != null) icon.sprite = food.icon;
    }

    public void OnOrderClicked()
    {
        if (customer != null)
        {
            OrderManager.Instance.AddOrder(customer.GetOrderedFood());
            customer.CompleteOrder();
        }
    }
}









