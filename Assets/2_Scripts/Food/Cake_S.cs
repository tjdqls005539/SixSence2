using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake_S : Food_SysTem
{
    public float _cookCake = 3f;
    private void OnMouseUp()
    {
        gameM.instance._stoveMenu = false;
        gameM.instance._cookTime = _cookCake;
        _stoveSystem._isCooking = true;
        _stoveSystem._cookSlider.maxValue = _cookCake;
        _stoveSystem._foodName = "Cake";
    }
}
