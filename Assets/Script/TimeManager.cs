using UnityEngine;
using TMPro;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;

    [Header("게임 기본 설정")]
    public float gameDuration = 30f;

    [Header("UI 오브젝트")]
    public TextMeshProUGUI timerText;

    [Header("패널 설정")]
    public GameObject ClosingPanel;  

    private float timer;
    private bool gameActive = false;

    // ClosePanel 키 입력 시 호출될 이벤트
    public Action OnClosingPanelAnyKey;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        timer = gameDuration;
        if (ClosingPanel != null)
            ClosingPanel.SetActive(false);

        UpdateTimerUI();
    }

    void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                timer = 0f;
                EndGame();
            }
            UpdateTimerUI();
            return;
        }

        // 게임 종료 상태 + ClosingPanel 활성 시 키 입력 감지
        if (ClosingPanel != null && ClosingPanel.activeSelf)
        {
            if (Input.anyKeyDown)
            {
                ClosingPanel.SetActive(false);
                OnClosingPanelAnyKey?.Invoke(); // 이벤트 호출
            }
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = Mathf.Ceil(timer).ToString();
    }

    public void StartGame()
    {
        gameActive = true;
        Debug.Log("게임 시작");
    }

    public void ResetTimer()
    {
        timer = gameDuration;
        UpdateTimerUI();
        gameActive = false;
    }

    void EndGame()
    {
        gameActive = false;

        if (ClosingPanel != null)
            ClosingPanel.SetActive(true);

        Debug.Log("게임 종료 - ClosePanel 대기");
    }

    public bool IsGameActive()
    {
        return gameActive;
    }
}







































