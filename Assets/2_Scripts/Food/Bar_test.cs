using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_test : MonoBehaviour
{
    void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }
    void Update()
    {
        if (gameM.instance._stoveMenu)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
