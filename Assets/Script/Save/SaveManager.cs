using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public GameObject SaveListPanel; // 인스펙터에서 연결

    void Awake()
    {
        // 처음 실행 시 SaveListPanel 비활성화
        if (SaveListPanel != null)
            SaveListPanel.SetActive(false);
    }

    public void SaveData(int slotIndex)
    {
        Debug.Log("슬롯 " + slotIndex + " 번에 저장되었습니다!");
    }
}


