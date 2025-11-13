using UnityEngine;

public class CC : MonoBehaviour
{
    public bool isOccupied = false;
    public bool isReserved = false;
    public bool hasDish = false; // 접시 존재 여부

    public Vector2 sitDirection = Vector2.up;

    public bool Reserve()
    {
        // 비어 있고 예약 중이 아니며 접시도 없을 때 예약 가능
        if (!isOccupied && !isReserved && !hasDish)
        {
            isReserved = true;
            return true;
        }
        return false;
    }

    public void Sit()
    {
        isOccupied = true;
        isReserved = false;
        Debug.Log(gameObject.name + "에 손님이 앉았습니다.");
    }

    public void Leave()
    {
        isOccupied = false;
        // hasDish는 그대로 두어 다른 손님이 못 앉게 유지
        Debug.Log(gameObject.name + "에서 손님이 떠났습니다.");
    }
}





















