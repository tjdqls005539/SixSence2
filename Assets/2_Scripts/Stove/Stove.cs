using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class FoodMenu
{
    public string foodName;
    public GameObject foodPrefab;
    public float cookTime;
    //'필요한 재료', '가격' 등을 추가 가능
}

public class Stove : MonoBehaviour
{

    [Header("요리 설정")]
    public List<FoodMenu> foodRecipe;
    
    [Header("태운 요리")]
    public GameObject burntFood;
    public float burntTime;

    [Header("위치 설정")]
    public Transform foodSpawnPoint;
    
    [Header("UI 설정")]
    public Slider cookingSlider;

    private bool isCooking = false;

    private void Start()
    {
        if (cookingSlider != null)
        {
            cookingSlider.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        //// 1. UI 위를 클릭했다면 무시 (중요)
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    return;
        //}
        if (isCooking)
        {
            return;
        }
        if (IsFoodAlreadyAtSpawnPoint())
        {
            return;
        }
        if (MenuManager.Instance.GetCurrentStove() == this)
        {
            MenuManager.Instance.HideMenu();
        }
        else
        {
            MenuManager.Instance.ShowMenu(this);
        }
    }

    public void StartCooking(string foodName)
    {
        if (isCooking) return;

        FoodMenu recipe = foodRecipe.Find(r => r.foodName == foodName);

        if (recipe != null)
        {
            StartCoroutine(CookRoutine(recipe.foodPrefab, recipe.cookTime));
        }
        else
        {
            Debug.LogError(foodName + "에 해당하는 레시피를 찾을 수 없습니다!");
        }
    }

    private IEnumerator CookRoutine(GameObject foodToCook, float cookTime)
    {
        isCooking = true;

        bool useSlider = cookingSlider != null && cookTime > 0;

        if (useSlider)
        {
            cookingSlider.gameObject.SetActive(true);
            cookingSlider.fillRect.GetComponent<Image>().color = Color.yellow;
            cookingSlider.value = 0;

            float elapsedTime = 0f;
            while (elapsedTime < cookTime)
            {
                elapsedTime += Time.deltaTime;
                cookingSlider.value = elapsedTime / cookTime;
                yield return null;
            }
            cookingSlider.gameObject.SetActive(false);
        }
        else if (cookTime > 0)
        {
            yield return new WaitForSeconds(cookTime);
        }

        GameObject spawnedFood = null;
        if (foodSpawnPoint != null && foodToCook != null)
        {
            spawnedFood = Instantiate(foodToCook, foodSpawnPoint.position, foodSpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("스폰 포인트 또는 음식 프리팹이 설정되지 않았습니다!");
            isCooking = false;
            yield break;
        }

        if (burntTime > 0 && burntFood != null)
        {
            Debug.Log(foodToCook.name + "이(가) 타기 시작합니다... (" + burntTime + "초)");

            float burntElapsedTime = 0f;

            while (burntElapsedTime < burntTime)
            {
                burntElapsedTime += Time.deltaTime;

                if (spawnedFood == null)
                {
                    isCooking = false;
                    yield break;
                }
                yield return null;
            }

            if (spawnedFood != null)
            {
                Destroy(spawnedFood);
                Instantiate(burntFood, foodSpawnPoint.position, foodSpawnPoint.rotation);
            }
        }
        isCooking = false; // 스토브 사용 가능
    }
    private bool IsFoodAlreadyAtSpawnPoint()
    {
        float checkRadius = 0.1f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(foodSpawnPoint.position, checkRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Food") || hit.CompareTag("BurntFood"))
            {
                return true;
            }
        }

        return false; // 음식이 없다.
    }
}