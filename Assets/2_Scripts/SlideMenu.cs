using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SlideMenu : MonoBehaviour
{
    [Header("슬라이드 설정")]
    public float slideDuration = 0.4f;

    [Header("시작/종료 위치")]
    public float hiddenXPosition = -500f;
    public float shownXPosition = 0f;

    // --- ?? 추가된 변수 ?? ---
    [Header("배경 흐림 설정")]
    [Tooltip("메뉴가 열릴 때 어둡게 할 CanvasGroup")]
    public CanvasGroup dimBackground;
    // --- ?? 추가된 변수 ?? ---

    private RectTransform panelRectTransform;
    private bool isShown = false;
    private Coroutine slideCoroutine;

    void Awake()
    {
        panelRectTransform = GetComponent<RectTransform>();
        panelRectTransform.anchoredPosition = new Vector2(hiddenXPosition, panelRectTransform.anchoredPosition.y);
        isShown = false;

        // --- ?? 추가된 초기화 코드 ?? ---
        // 시작할 때 dim 배경을 투명하고 클릭 불가능하게 설정
        if (dimBackground != null)
        {
            dimBackground.alpha = 0f;
            dimBackground.blocksRaycasts = false; // 클릭 방지 해제
        }
        // --- ?? 추가된 초기화 코드 ?? ---
    }

    /// <summary>
    /// (UI 버튼에서 호출) 메뉴를 열거나 닫습니다.
    /// </summary>
    public void ToggleMenu()
    {
        float targetX;
        float targetAlpha; // ?? dim 배경의 목표 알파값

        if (isShown)
        {
            // 닫기
            targetX = hiddenXPosition;
            targetAlpha = 0f;
        }
        else
        {
            // 열기
            targetX = shownXPosition;
            targetAlpha = 1f;
        }

        isShown = !isShown;

        // ?? 목표 알파값을 StartSlide로 전달
        StartSlide(targetX, targetAlpha);
    }

    // --- ?? 수정된 함수 ?? ---
    // (float targetAlpha 매개변수 추가)
    private void StartSlide(float targetX, float targetAlpha)
    {
        if (slideCoroutine != null)
        {
            StopCoroutine(slideCoroutine);
        }

        Vector2 targetPosition = new Vector2(targetX, panelRectTransform.anchoredPosition.y);

        // ?? dim 배경 클릭 방지 설정
        if (dimBackground != null)
        {
            // 만약 열리는 중(targetAlpha > 0)이라면, 
            // 즉시 클릭을 막기 시작합니다.
            if (targetAlpha > 0)
            {
                dimBackground.blocksRaycasts = true;
            }
            // (닫힐 때는 애니메이션이 끝난 후 클릭 방지를 풉니다)
        }

        // ?? 코루틴에도 목표 알파값 전달
        slideCoroutine = StartCoroutine(SlideRoutine(targetPosition, targetAlpha));
    }

    // --- ?? 수정된 코루틴 ?? ---
    // (float targetAlpha 매개변수 추가)
    private IEnumerator SlideRoutine(Vector2 targetPosition, float targetAlpha)
    {
        float elapsedTime = 0f;
        Vector2 startPosition = panelRectTransform.anchoredPosition;

        // ?? dim 배경의 시작 알파값 가져오기
        float startAlpha = 0f;
        if (dimBackground != null)
        {
            startAlpha = dimBackground.alpha;
        }

        while (elapsedTime < slideDuration)
        {
            float t = elapsedTime / slideDuration;
            // t = Mathf.SmoothStep(0, 1, t); // 부드러운 효과

            // 1. 패널 위치 보간
            panelRectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, t);

            // 2. ?? Dim 배경 알파값 보간
            if (dimBackground != null)
            {
                dimBackground.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 3. 애니메이션 종료 후 값 고정
        panelRectTransform.anchoredPosition = targetPosition;
        if (dimBackground != null)
        {
            dimBackground.alpha = targetAlpha;

            // ?? 닫기가 완료되었다면(targetAlpha == 0), 
            // 클릭 방지를 다시 해제합니다.
            if (targetAlpha == 0)
            {
                dimBackground.blocksRaycasts = false;
            }
        }

        slideCoroutine = null;
    }
}