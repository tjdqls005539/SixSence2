using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Stove : StoveLevelSystem
{
    [Header("위치 설정")]
    public Transform foodSpawnPoint;
    
    [Header("UI 설정")]
    public Slider cookingSlider;

    private bool isCooking = false;
    private void Start()
    {
        StoveSetting();
        if (cookingSlider != null)
        {
            cookingSlider.gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
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
    public override void StoveSetting()
    {
        if (_data._level == 0)
        {
            _data._level = 1;
            _data._burnTime = 0f;
            _data._foodTime = 0f;
        }
    }

    public void StartCooking(FoodSystem foodData)
    {
        if (isCooking) return;


        if (foodData != null)
        {
            StartCoroutine(CookRoutine(foodData));
        }
        else
        {
            Debug.LogError(foodData.foodName + "에 해당하는 레시피를 찾을 수 없습니다!");
        }
    }

    private IEnumerator CookRoutine(FoodSystem foodData)
    {
        isCooking = true;

        GameObject prefabToSpawn = foodData.foodPrefab;
        GameObject burntFoodToSpawn = foodData.burntFoodPrefab;

        float cookTime = foodData.cookTime - _data._foodTime;
        float burntTimeForThisFood = foodData.burntTime + _data._burnTime;

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
        if (foodSpawnPoint != null && prefabToSpawn != null)
        {
            spawnedFood = Instantiate(prefabToSpawn, foodSpawnPoint.position, foodSpawnPoint.rotation);
        }
        else
        {
            Debug.LogError("스폰 포인트 또는 음식 프리팹이 설정되지 않았습니다!");
            isCooking = false;
            yield break;
        }

        if (burntTimeForThisFood > 0 && burntFoodToSpawn != null)
        {
            float burntElapsedTime = 0f;
            while (burntElapsedTime < burntTimeForThisFood)
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
                Instantiate(burntFoodToSpawn, foodSpawnPoint.position, foodSpawnPoint.rotation);
            }
        }
        isCooking = false;
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
    public void OnClick_UpgradeAllStoves()
    {
        if (StoveManager.Instance != null)
        {
            StoveManager.Instance.UpgradeAllStoves();
        }
        else
        {
            Debug.LogError("StoveManager가 씬에 없습니다! 씬에 StoveManager 오브젝트를 추가했는지 확인하세요.");
        }
    }
}