using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FoodSystem : MonoBehaviour
{
    [Header("요리 정보")]
    public string foodName;
    public GameObject foodPrefab;
    public float cookTime;

    [Header("타는 설정")]
    public GameObject burntFoodPrefab;
    public float burntTime;

 
    public virtual void OnClick_CookFood()
    {
        Stove stoveToUse = MenuManager.Instance.GetCurrentStove();
        
        if (stoveToUse != null)
        {
            stoveToUse.StartCooking(this);

            MenuManager.Instance.HideMenu();
        }
        else
        {
            Debug.LogError("현재 선택된 스토브가 없습니다!");
        }
    }
}
