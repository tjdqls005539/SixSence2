using UnityEngine;
using TMPro; // TMP_Text 사용

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int totalMoney = 0;

    public TMP_Text moneyText; // 기존 Text 대신 TMP_Text

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = $"누적 매출: {totalMoney}원";
    }
}



