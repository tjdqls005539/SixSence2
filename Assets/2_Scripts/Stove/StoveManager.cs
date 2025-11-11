using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveManager : MonoBehaviour
{
    public static StoveManager Instance;

    private List<Stove> allStovesInScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        FindAndRegisterAllStoves();
    }

    private void FindAndRegisterAllStoves()
    {
        allStovesInScene = new List<Stove>(FindObjectsOfType<Stove>());
    }

    public void UpgradeAllStoves()
    {
        if (gameM.instance._gold < 100)
        {
            Debug.Log("업그레이드 비용이 부족합니다!");
            return;
        }
        gameM.instance._gold -= 100;

        foreach (Stove stove in allStovesInScene)
        {
            stove.UpgradeStove();
        }
    }
    public Stove GetReferenceStove()
    {
        if (allStovesInScene != null && allStovesInScene.Count > 0)
        {
            return allStovesInScene[0];
        }
        return null;
    }
}