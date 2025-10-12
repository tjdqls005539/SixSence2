using UnityEngine;

public class CC : MonoBehaviour
{
    public bool isOccupied = false;
    public bool isReserved = false;

    // 손님이 앉을 때 바라볼 방향
    public Vector2 sitDirection = Vector2.up;

    public bool Reserve()
    {
        if (!isOccupied && !isReserved)
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
        Debug.Log(gameObject.name + "에서 손님이 떠났습니다.");
    }
}




















