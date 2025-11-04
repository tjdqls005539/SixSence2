using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake : MonoBehaviour
{
    private void OnMouseDown()
    {
        MenuManager.Instance.OnClick_CookCake();
    }
}
