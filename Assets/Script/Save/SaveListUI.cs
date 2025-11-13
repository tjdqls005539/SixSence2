using UnityEngine;
using UnityEngine.UI;

public class SaveListUI : MonoBehaviour
{
    public Text[] slotLabels; // 슬롯 내부의 텍스트 UI들 1,2,3,4 순으로 연결

    public void RefreshUI()
    {
        for (int i = 0; i < slotLabels.Length; i++)
        {
            int slotIndex = i + 1;

            // 저장 데이터 여부 확인
            if (PlayerPrefs.HasKey("SaveSlot" + slotIndex))
            {
                string time = PlayerPrefs.GetString("SaveSlotTime" + slotIndex);
                slotLabels[i].text = "슬롯 " + slotIndex + "\n저장시간: " + time;
            }
            else
            {
                slotLabels[i].text = "슬롯 " + slotIndex + "\n(빈 슬롯)";
            }
        }
    }

    void OnEnable()
    {
        RefreshUI(); // 패널 열 때 자동 업데이트
    }
}

