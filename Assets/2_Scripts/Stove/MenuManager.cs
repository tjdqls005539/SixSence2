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
}