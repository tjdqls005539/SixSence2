using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance; // ✅ 싱글톤

    public float gameDuration = 30f; 
    public GameObject resultPanel;
    public TextMeshProUGUI timerText;

    public TMP_Text visitorsText;
    public TMP_Text totalMoneyText;
    public TMP_Text roundEarningsText;

    private float timer;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timer = gameDuration;
        resultPanel.SetActive(false);
        Time.timeScale = 1f;
        UpdateTimerUI();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            timer = 0f;
            EndGame();
        }

        UpdateTimerUI();
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(timer).ToString();
    }

    public float GetRemainingTime() // ✅ 외부에서 접근 가능한 함수
    {
        return timer;
    }

    void EndGame()
    {
        Time.timeScale = 0f;
        resultPanel.SetActive(true);

        visitorsText.text = $"방문 손님 수: {MoneyManager.Instance.visitedCustomersCount}명";
        totalMoneyText.text = $"현재 보유 금액: {MoneyManager.Instance.totalMoney}원";
        roundEarningsText.text = $"이번 라운드 수익: {MoneyManager.Instance.roundEarnings}원";

        Debug.Log("게임 종료 - 결산 패널 표시");
    }
}





