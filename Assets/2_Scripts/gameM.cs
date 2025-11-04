using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameM : MonoBehaviour
{
    public static gameM instance = null;
    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
