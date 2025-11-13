using UnityEngine;
using System;

public class SaveTimeManager : MonoBehaviour
{
    public void SaveData(int slotIndex)
    {
        PlayerPrefs.SetString("SaveSlot" + slotIndex, "PlayerDataExample");

        // 저장 시간 기록
        PlayerPrefs.SetString("SaveSlotTime" + slotIndex, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

        PlayerPrefs.Save();
        Debug.Log(slotIndex + " 번 슬롯에 저장 완료!");

        // 저장 후 슬롯 UI 업데이트
        FindObjectOfType<SaveListUI>().RefreshUI();
    }
}


