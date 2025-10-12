using UnityEngine;
using UnityEngine.SceneManagement;  // 씬 이동용

public class MainMenu : MonoBehaviour
{
    // 시작하기 버튼
    public void OnStartButton()
    {
        // 예: 게임 첫 번째 스테이지로 이동
        SceneManager.LoadScene("GameScene");
    }

    // 계속하기 버튼
    public void OnContinueButton()
    {
        // 저장된 데이터 불러오기 (PlayerPrefs 예시)
        if (PlayerPrefs.HasKey("SavedScene"))
        {
            string sceneName = PlayerPrefs.GetString("SavedScene");
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.Log("저장된 게임이 없습니다!");
        }
    }

    // 설정 버튼
    public void OnSettingsButton()
    {
        // 설정 UI 패널 열기 (패널 활성화 예시)
        Debug.Log("설정창 열기");
    }

    // 종료 버튼
    public void OnExitButton()
    {
        Debug.Log("게임 종료");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서만 종료
#endif
    }
}

