using UnityEngine;
using TMPro;
using System.Collections;

public class WeeklyPaymentManager : MonoBehaviour
{
    public static WeeklyPaymentManager Instance;

    [Header("게임 기본 설정")]
    public int baseAmount = 1000;
    public int dayCounter = 0;

    [Header("UI 슬롯")]
    public TMP_Text todayDayText;
    public TMP_Text currentWeekAmountText;
    public TMP_Text nextWeekAmountText;
    public TMP_Text daysUntilPaymentText;

    [Header("패널 설정")]
    public GameObject OpeningPanel;
    public GameObject ResultPanel;

    private string[] daysOfWeek = { "월", "화", "수", "목", "금", "토", "일" };
    private bool isOpeningActive = false;
    private bool inputLock = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        UpdateUI();

        // OpeningPanel 켜기
        if (OpeningPanel != null)
        {
            OpeningPanel.SetActive(true);
            isOpeningActive = true;
        }

        // ResultPanel 시작 시 꺼주기
        if (ResultPanel != null)
            ResultPanel.SetActive(false);

        // TimeManager 이벤트 구독
        if (TimeManager.Instance != null)
            TimeManager.Instance.OnClosingPanelAnyKey += ShowResultPanel;
    }

    void Update()
    {
        if (isOpeningActive && !inputLock && Input.GetKeyDown(KeyCode.Space))
        {
            if (OpeningPanel != null)
                OpeningPanel.SetActive(false);

            isOpeningActive = false;
            StartBusiness();
        }
    }

    void UpdateUI()
    {
        int currentDayIndex = dayCounter % 7;
        int currentWeek = dayCounter / 7;

        int currentWeekAmount = baseAmount + currentWeek * 100;
        int nextWeekAmount = baseAmount + (currentWeek + 1) * 100;
        int daysUntilNextPayment = 7 - (currentDayIndex + 1);

        if (todayDayText != null)
            todayDayText.text = $"오늘의 요일 : {daysOfWeek[currentDayIndex]}요일";
        if (currentWeekAmountText != null)
            currentWeekAmountText.text = $"이번 주 할당금: {currentWeekAmount}원";
        if (nextWeekAmountText != null)
            nextWeekAmountText.text = $"다음 주 할당금: {nextWeekAmount}원";
        if (daysUntilPaymentText != null)
            daysUntilPaymentText.text = $"다음 할당금까지 남은 일 수: {daysUntilNextPayment}일";
    }

    public void AdvanceDay()
    {
        dayCounter++;
        UpdateUI();
    }

    void StartBusiness()
    {
        if (TimeManager.Instance != null)
            TimeManager.Instance.StartGame();

        CustomerSpawner spawner = FindObjectOfType<CustomerSpawner>();
        if (spawner != null)
            spawner.StartSpawning();
    }

    // TimeManager ClosePanel 키 입력 후 호출
    void ShowResultPanel()
    {
        if (ResultPanel != null)
            ResultPanel.SetActive(true);
    }

    // ResultPanel에서 다음 날로 넘어갈 때 호출
    public void PrepareNextDay()
    {
        // ResultPanel 닫기
        if (ResultPanel != null && ResultPanel.activeSelf)
            ResultPanel.SetActive(false);

        // 이전 라운드 손님 제거
        Customer[] remainingCustomers = FindObjectsOfType<Customer>();
        foreach (Customer c in remainingCustomers)
            Destroy(c.gameObject);

        // 날짜 증가
        AdvanceDay();

        // 타이머 초기화
        if (TimeManager.Instance != null)
            TimeManager.Instance.ResetTimer();

        // MoneyManager 새 라운드 초기화
        if (MoneyManager.Instance != null)
            MoneyManager.Instance.StartNewRound();

        // OpeningPanel 켜기
        if (OpeningPanel != null)
            StartCoroutine(WaitAndEnableInput());
    }

    IEnumerator WaitAndEnableInput()
    {
        inputLock = true;
        isOpeningActive = true;
        OpeningPanel.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        inputLock = false;
    }
}














































