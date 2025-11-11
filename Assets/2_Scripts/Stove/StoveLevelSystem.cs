using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public struct StoveData
{
    public int _level;
    public float _burnTime;
    public float _foodTime;
}
public abstract class StoveLevelSystem : MonoBehaviour
{
    public StoveData _data;
    public int _maxLev = 3;
    public abstract void StoveSetting();
    public virtual void UpgradeStove()
    {
        if (_data._level >= _maxLev)
        {
            return;
        }

        _data._level += 1;
        _data._burnTime += 2;
        _data._foodTime += 2; 
    }
}
