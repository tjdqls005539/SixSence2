using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steak : MonoBehaviour
{
    private void OnMouseDown()
    {
        MenuManager.Instance.OnClick_CookSteak();
    }
}
