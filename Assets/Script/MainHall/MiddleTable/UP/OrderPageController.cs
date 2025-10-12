using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class OrderPageController : MonoBehaviour
{
    [Header("UI 연결")]
    public GameObject orderPagePanel;       // 주문 페이지 패널 (전체)
    public Transform contentParent;         // ScrollView Content
    public GameObject orderItemPrefab;      // 주문 아이템 프리팹 (이름 + 아이콘)
    public Button openButton;               // "주문 보기" 버튼
    public Button closeButton;              // "닫기" 버튼
    public TextMeshProUGUI totalOrderText;  // 총 주문 수 표시 텍스트

    private List<GameObject> spawnedOrders = new List<GameObject>();

    void Start()
    {
        if (orderPagePanel != null)
            orderPagePanel.SetActive(false); // 시작 시 패널 숨김

        if (openButton != null)
            openButton.onClick.AddListener(ShowOrderPage);

        if (closeButton != null)
            closeButton.onClick.AddListener(HideOrderPage);
    }

    // 주문 페이지 열기
    public void ShowOrderPage()
    {
        if (orderPagePanel != null)
            orderPagePanel.SetActive(true);

        Time.timeScale = 0f; // 게임 일시정지

        ClearOrders();

        List<Food> orders = OrderManager.Instance.GetOrders();
        if (totalOrderText != null)
            totalOrderText.text = $"총 주문 수: {orders.Count}";

        foreach (Food food in orders)
        {
            GameObject item = Instantiate(orderItemPrefab, contentParent);

            // 이름 설정
            Transform nameObj = item.transform.Find("Name");
            if (nameObj != null)
            {
                TextMeshProUGUI nameText = nameObj.GetComponent<TextMeshProUGUI>();
                if (nameText != null)
                    nameText.text = food.foodName;
            }
            else
            {
                Debug.LogWarning("OrderItemPrefab 안에 'Name' 오브젝트를 찾을 수 없습니다.");
            }

            // 아이콘 설정
            Transform iconObj = item.transform.Find("Icon");
            if (iconObj != null)
            {
                Image iconImage = iconObj.GetComponent<Image>();
                if (iconImage != null)
                    iconImage.sprite = food.icon;
            }
            else
            {
                Debug.LogWarning("OrderItemPrefab 안에 'Icon' 오브젝트를 찾을 수 없습니다.");
            }

            spawnedOrders.Add(item);
        }
    }

    // 주문 페이지 닫기
    public void HideOrderPage()
    {
        if (orderPagePanel != null)
            orderPagePanel.SetActive(false);

        Time.timeScale = 1f; // 게임 재개
        ClearOrders();
    }

    private void ClearOrders()
    {
        foreach (var obj in spawnedOrders)
            Destroy(obj);
        spawnedOrders.Clear();
    }
}







