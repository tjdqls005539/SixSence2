using UnityEngine;
using System.Collections.Generic;

public class CCManager : MonoBehaviour
{
    public static CCManager Instance;
    public List<CC> chairs;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public CC GetAvailableChair()
    {
        foreach (CC c in chairs)
        {
            if (!c.isOccupied && !c.isReserved)
            {
                return c;
            }
        }
        return null;
    }
}















