using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }

    private Stove currentStove;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    public void ShowMenu(Stove stove)
    {
        this.currentStove = stove;

        gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        gameObject.SetActive(false);
        currentStove = null; // 참조 초기화
    }
    public Stove GetCurrentStove()
    {
        return currentStove;
    }

    public void OnClick_CookCake()
    {
        if (currentStove != null)
        {
            currentStove.StartCooking("Cake");

            HideMenu();
        }
        else
        {
            Debug.LogError("현재 선택된 스토브가 없습니다!");
        }
    }

    public void OnClick_CookSteak()
    {
        if (currentStove != null)
        {
            currentStove.StartCooking("Steak");

            HideMenu();
        }
        else
        {
            Debug.LogError("현재 선택된 스토브가 없습니다!");
        }
    }
    public void OnClick_CookSoop()
    {
        if (currentStove != null)
        {
            currentStove.StartCooking("Soop");

            HideMenu();
        }
        else
        {
            Debug.LogError("현재 선택된 스토브가 없습니다!");
        }
    }
    //public void OnClick_CookOmelet()
    //{
    //    if (currentStove != null)
    //    {
    //        currentStove.StartCooking("Omelet");

    //        HideMenu();
    //    }
    //    else
    //    {
    //        Debug.LogError("현재 선택된 스토브가 없습니다!");
    //    }
    //}
}