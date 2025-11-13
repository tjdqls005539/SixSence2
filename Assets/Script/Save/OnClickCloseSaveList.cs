using UnityEngine;

public class OnClickCloseSaveList : MonoBehaviour
{
    public GameObject saveListPanel;

    public void OnClickClose()
    {
        saveListPanel.SetActive(false);
    }
}

