using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public GameObject upgradePanel;

    void Awake()
    {
        if (upgradePanel != null)
            upgradePanel.SetActive(false); // 처음에는 비활성화
    }

    // 버튼 클릭 시 호출
    public void OpenUpgradePanel()
    {
        if (upgradePanel != null)
            upgradePanel.SetActive(true);
    }

    // 필요하면 닫기 함수도 추가
    public void CloseUpgradePanel()
    {
        if (upgradePanel != null)
            upgradePanel.SetActive(false);
    }
}

