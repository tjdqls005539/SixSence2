using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class DraggableFood2D : MonoBehaviour
{
    private Vector3 offset;
    private bool dragging = false;
    private SpriteRenderer spriteRenderer;
    private int originalSortingOrder;
    private Rigidbody2D rb;

    [Header("손님 음식 전달 관련")]
    public float deliveryRadius = 0.5f; // 디버그용, Trigger 대신 Overlap 검사 가능

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;

        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            originalSortingOrder = spriteRenderer.sortingOrder;
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

    void FixedUpdate()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 targetPos = new Vector2(mousePos.x, mousePos.y) + (Vector2)offset;
            rb.MovePosition(targetPos);
        }
    }

    // Trigger 기반 손님 감지
    void OnTriggerEnter2D(Collider2D collision)
    {
        Customer customer = collision.GetComponent<Customer>();
        if (customer != null && customer.IsSeated)
        {
            Food orderedFood = customer.GetOrderedFood();
            if (orderedFood != null)
            {
                string cleanName = gameObject.name.Replace("(Clone)", "").Trim();
                if (orderedFood.foodName == cleanName)
                {
                    // 음식 전달
                    customer.ReceiveFood(gameObject);
                }
            }
        }
    }

    // 씬에서 확인용
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, deliveryRadius);
    }
}









