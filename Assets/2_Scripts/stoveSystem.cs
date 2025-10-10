using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stoveSystem : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("Å¬¸¯");
            gameM.instance._stoveMenu = true;
        }
    }
}
