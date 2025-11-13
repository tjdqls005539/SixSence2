using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dish : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging = false;
    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;

    //  손님이 앉았던 의자 연결용
    [HideInInspector] public CC linkedChair;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalSortingOrder = spriteRenderer.sortingOrder;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;
    }

    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
        dragging = true;

        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = 1000;
    }

    void OnMouseUp()
    {
        dragging = false;

        if (spriteRenderer != null)
            spriteRenderer.sortingOrder = originalSortingOrder;
    }

    void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
        }
    }
}






