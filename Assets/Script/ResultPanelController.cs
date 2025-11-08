using UnityEngine;
using TMPro;

public class ResultPanelController : MonoBehaviour
{
    public TextMeshProUGUI customerCountText;
    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI earnedThisRoundText;

    void OnEnable()
    {
        customerCountText.text = GameDataManager.Instance.customerCount.ToString();
        totalMoneyText.text = GameDataManager.Instance.totalMoney.ToString();
        earnedThisRoundText.text = GameDataManager.Instance.earnedThisRound.ToString();
    }
}
