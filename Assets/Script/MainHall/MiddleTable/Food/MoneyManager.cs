using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    public int totalMoney = 0;
    
    public int visitedCustomersCount = 0; // 방문 손님 수 기록
    public int moneyAtGameStart = 0; // 라운드 시작 전 금액
    public int roundEarnings = 0; // 이번 라운드 동안 번 금액
    
    public TMP_Text moneyText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    public void StartNewRound()
{
    visitedCustomersCount = 0;
    roundEarnings = 0;
    moneyAtGameStart = totalMoney; // 라운드 시작 시 기준 금액 갱신
    Debug.Log("MoneyManager: 새 라운드 시작");
}


    private void Start()
    {
        moneyAtGameStart = totalMoney; // 게임 시작 시 현재 금액 저장
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        roundEarnings = totalMoney - moneyAtGameStart;
        UpdateUI();
    }

    public void AddVisitor()
    {
        visitedCustomersCount++;
    }

    private void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = $"누적 매출: {totalMoney}원";
    }
}




