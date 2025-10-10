using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Food_SysTem : MonoBehaviour
{
    public float _time = 0f;    
    protected StoveSystem _stoveSystem;
    protected SpriteRenderer _spriteRenderer;
    public void Start()
    {
        _stoveSystem = FindObjectOfType<StoveSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    //public abstract void Cooking();
}
