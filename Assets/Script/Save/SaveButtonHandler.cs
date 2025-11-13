using UnityEngine;

public class SaveButtonHandler : MonoBehaviour
{
    public GameObject saveListPanel; // 세이브 목록 패널

    public void OnClickSaveButton()
    {
        saveListPanel.SetActive(true); // 패널 보이기
    }
}

