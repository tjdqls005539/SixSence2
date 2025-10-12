using UnityEngine;

public class FoodOrderPageController : MonoBehaviour
{
    [Header("패널 및 참조")]
    public GameObject FoodOrderPanel;   // 메뉴 패널(UI)
    public Food[] foods;                // 음식 데이터 배열
    public Transform oven;              // 음식 생성 기준 오브젝트 (Oven)
    public Transform foodParent;        // 음식들이 생성될 부모 오브젝트 (Canvas 밖)

    [Header("생성 위치 옵션")]
    public Vector3 spawnOffset = new Vector3(0f, 0.5f, 0f); // 오븐 위쪽 약간 띄우기

    void Start()
    {
        if (FoodOrderPanel != null)
            FoodOrderPanel.SetActive(false);
    }

    // 메뉴 토글 버튼
    public void ToggleMenuPanel()
    {
        if (FoodOrderPanel != null)
            FoodOrderPanel.SetActive(!FoodOrderPanel.activeSelf);
    }

    // 닫기 버튼
    public void CloseMenuPanel()
    {
        if (FoodOrderPanel != null)
            FoodOrderPanel.SetActive(false);
    }

    // 음식 버튼 클릭
    public void OnClickMenu(int index)
    {
        if (index < 0 || index >= foods.Length)
        {
            Debug.LogWarning("잘못된 음식 인덱스입니다!");
            return;
        }

        Food selectedFood = foods[index];

        if (selectedFood.prefab != null && oven != null)
        {
            //  오븐 위치 기준으로 생성
            Vector3 spawnPos = oven.position + spawnOffset;

            Transform parent = foodParent != null ? foodParent : null;
            GameObject newFood = Instantiate(selectedFood.prefab, spawnPos, Quaternion.identity, parent);

            newFood.transform.localScale = Vector3.one * 0.03f;

            if (!newFood.activeSelf)
                newFood.SetActive(true);

            Debug.Log($"{selectedFood.foodName} 생성 완료! 위치: {newFood.transform.position}");

            if (newFood.GetComponent<Collider2D>() == null)
                Debug.LogWarning($"{newFood.name}에는 Collider2D가 없습니다!");
            else
                Debug.Log($"{newFood.name}에 Collider2D 정상 감지됨!");
        }
        else
        {
            Debug.LogWarning($"{selectedFood.foodName}의 prefab 또는 Oven 참조가 비어 있습니다!");
        }
    }

    void OnDrawGizmos()
    {
        if (oven != null)
        {
            Gizmos.color = Color.red;
            Vector3 spawnPos = oven.position + spawnOffset;
            Gizmos.DrawSphere(spawnPos, 0.1f);
        }
    }
}
















