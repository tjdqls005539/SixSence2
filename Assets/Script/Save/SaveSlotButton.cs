using UnityEngine;

public class SaveSlotButton : MonoBehaviour
{
    public int slotIndex;
    public SaveManager saveManager;

    public void OnClickSlot()
    {
        saveManager.SaveData(slotIndex);
    }
}

