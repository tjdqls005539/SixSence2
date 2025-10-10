using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stake_S : Food_SysTem
{
    public float _cookStake = 5f;
    private void OnMouseUp()
    {
        gameM.instance._stoveMenu = false;
        gameM.instance._cookTime = _cookStake;
        _stoveSystem._isCooking = true;
        _stoveSystem._cookSlider.maxValue = _cookStake;
        _stoveSystem._foodName = "Stake";
    }
}
