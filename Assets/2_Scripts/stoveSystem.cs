using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveSystem : MonoBehaviour
{
    public float _time = 0f;
    public bool _isCooking = false;
    public string _foodName;
    public Slider _cookSlider;
    void Start()
    {
        // Cooking Time Bar Init
        _cookSlider = GetComponentInChildren<Slider>();
        _cookSlider.maxValue = 0;
        _cookSlider.minValue = 0;
        _cookSlider.value = _cookSlider.minValue;
        _cookSlider.gameObject.SetActive(false);
    }
    public void OnMouseUp()
    {
        if (!_isCooking)
        {
            gameM.instance._stoveMenu = true;
        }
    }
    public void Update()
    {
        if (_isCooking)
        {
            _cookSlider.gameObject.SetActive(true);
            _time += Time.deltaTime;
            _cookSlider.value = _time;
            StartCooking(_cookSlider.maxValue);
        }
    }
    public void StartCooking(float _cookTime)
    {
        if (_time >= _cookTime)
        {
            _isCooking = false;
            _cookSlider.gameObject.SetActive(false);
            _time = 0f;
        }
    }

}
