using UnityEngine;
using TMPro;

public class ResultPanelController : MonoBehaviour
{
    public TextMeshProUGUI visitedCustomersCountText;
    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI roundEarningsText;

    void OnEnable()
    {
        if (MoneyManager.Instance == null)
        {
            Debug.LogError("MoneyManager.Instance가 없습니다!");
            return;
        }

        visitedCustomersCountText.text = $"이번 라운드 방문 손님 수: {MoneyManager.Instance.visitedCustomersCount}";
        totalMoneyText.text = $"현재 보유 금액: {MoneyManager.Instance.totalMoney}원";
        roundEarningsText.text = $"이번 라운드 수익: {MoneyManager.Instance.roundEarnings}원";
    }
}





