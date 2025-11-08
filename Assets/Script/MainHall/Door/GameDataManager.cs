using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;

    public int totalMoney = 0;      // 전체 보유 금액
    public int earnedThisRound = 0; // 이번 라운드에서 번 금액
    public int customerCount = 0;   // 이번 라운드에서 방문한 손님 수

    void Awake()
    {
        Instance = this;
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        earnedThisRound += amount;
    }

    public void AddCustomer()
    {
        customerCount++;
    }

    public void ResetRound()
    {
        earnedThisRound = 0;
        customerCount = 0;
    }
}

