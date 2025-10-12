using UnityEngine;

[CreateAssetMenu(fileName = "FoodData", menuName = "MainHall/Food")]
public class Food : ScriptableObject
{
    public string foodName;    // 음식 이름
    public Sprite icon;    // 음식 아이콘
    public GameObject prefab;   // 이 음식의 실제 프리팹
    public float price;         //  음식 가격
}

