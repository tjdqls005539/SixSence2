using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldSystem : MonoBehaviour
{
    TextMeshProUGUI _text;
    private void Start()
    {
        _text = this.GetComponent<TextMeshProUGUI>();
        gameM.instance._gold += 200f;
    }
    private void Update()
    {
        _text.text = "Gold : " + string.Format("{0:N0}", gameM.instance._gold);
    }
}
