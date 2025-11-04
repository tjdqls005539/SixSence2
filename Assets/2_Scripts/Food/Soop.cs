using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soop : MonoBehaviour
{
    private void OnMouseDown()
    {
        MenuManager.Instance.OnClick_CookSoop();
    }
}
