using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dish : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging = false;
    private Camera cam;

    // 손님이 앉아 있던 자리 연결
    public CC linkedChair;

    void Start()
    {
        cam = Camera.main;

        // 생성 시 자리 점유 상태 유지
        if (linkedChair != null)
            linkedChair.isOccupied = true;
    }

    void OnMouseDown()
    {
        if (cam == null) cam = Camera.main;
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
        dragging = true;
    }

    void OnMouseDrag()
    {
        if (!dragging) return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;
    }

    void OnMouseUp()
    {
        dragging = false;
    }

    // 접시가 치워질 때 호출 (예: 플레이어가 세척장에 드롭)
    public void ClearDish()
    {
        if (linkedChair != null)
            linkedChair.isOccupied = false;

        Destroy(gameObject);
    }
}

