using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Customer : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 2f;
    public LayerMask obstacleLayer;

    [Header("주문")]
    public Food[] foodOptions;
    public GameObject dishPrefab;

    [Header("참조")]
    public MoneyManager moneyManager;
    public Transform counterPosition;
    public Transform exitPositionTransform;

    [Header("주문 아이콘 설정")]
    public float iconScale = 0.5f;
    public float iconHeight = 1.5f;

    private Rigidbody2D rb;
    private CC targetChair;
    private Food orderedFood;
    private GameObject orderIconGO;

    private enum LeaveState { None, GoingToCounter, PayingAtCounter, GoingToExit }
    private LeaveState leaveState = LeaveState.None;

    private bool hasSat = false;
    private bool orderCompleted = false;
    private bool isEating = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        if (obstacleLayer == 0)
            obstacleLayer = LayerMask.GetMask("Obstacle");
    }

    public void StartFindingChair()
    {
        StartCoroutine(FindChairRoutine());
    }

    private IEnumerator FindChairRoutine()
    {
        while (targetChair == null)
        {
            CC chair = CCManager.Instance.GetAvailableChair();
            if (chair != null && chair.Reserve())
            {
                targetChair = chair;
                break;
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

    void Update()
    {
        if (targetChair != null && !hasSat)
        {
            MoveTowards(targetChair.transform.position);
            if (Vector2.Distance(rb.position, targetChair.transform.position) < 0.3f)
                SitOnChair();
        }
        else if (orderCompleted)
        {
            HandleLeaving();
        }
    }

    private void MoveTowards(Vector2 targetPos)
    {
        Vector2 dir = (targetPos - rb.position).normalized;

        // 장애물 회피
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(rb.position, 1.5f, obstacleLayer);
        foreach (var obs in obstacles)
        {
            Vector2 away = rb.position - (Vector2)obs.transform.position;
            dir += away.normalized / Mathf.Max(away.magnitude, 0.1f);
        }
        dir.Normalize();
        rb.MovePosition(rb.position + dir * moveSpeed * Time.deltaTime);
        rb.rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
    }

    private void SitOnChair()
    {
        if (targetChair == null || hasSat) return;

        hasSat = true;
        targetChair.Sit();
        rb.position = targetChair.transform.position;

        // 주문 선택
        if (foodOptions != null && foodOptions.Length > 0)
        {
            orderedFood = foodOptions[Random.Range(0, foodOptions.Length)];
            OrderManager.Instance?.AddOrder(orderedFood);
        }

        // 주문 아이콘 생성
        if (orderedFood?.icon != null)
        {
            if (orderIconGO != null) Destroy(orderIconGO);
            orderIconGO = new GameObject("OrderIcon");
            orderIconGO.transform.SetParent(transform);
            orderIconGO.transform.localPosition = Vector3.up * iconHeight;
            orderIconGO.transform.localScale = Vector3.one * iconScale;

            var sr = orderIconGO.AddComponent<SpriteRenderer>();
            sr.sprite = orderedFood.icon;
            sr.sortingLayerName = "UI";
            sr.sortingOrder = 100;
        }
    }

    // 음식 받으면 아이콘 제거 + 3초 식사
    public void ReceiveFood(GameObject deliveredFood = null)
    {
        if (isEating) return;
        isEating = true;

        if (orderIconGO != null)
        {
            Destroy(orderIconGO);
            orderIconGO = null;
        }

        if (deliveredFood != null)
            Destroy(deliveredFood);

        StartCoroutine(EatingRoutine());
    }

    private IEnumerator EatingRoutine()
    {
        // 3초 식사
        yield return new WaitForSeconds(3f);

        // 접시 생성
        CreateDishOnChair();

        orderCompleted = true;
        leaveState = LeaveState.GoingToCounter;
        isEating = false;
    }

    private void CreateDishOnChair()
    {
        if (dishPrefab != null && targetChair != null)
        {
            GameObject dish = Instantiate(dishPrefab, targetChair.transform.position, Quaternion.identity);
            Dish dishScript = dish.GetComponent<Dish>();
            if (dishScript != null)
                dishScript.linkedChair = targetChair;

            // 의자 상태 업데이트
            targetChair.hasDish = true;
        }
    }

    private void HandleLeaving()
    {
        Vector2 target = Vector2.zero;

        switch (leaveState)
        {
            case LeaveState.GoingToCounter:
                target = counterPosition.position;
                MoveTowards(target);
                if (Vector2.Distance(rb.position, target) < 0.1f)
                {
                    moneyManager?.AddMoney((int)(orderedFood?.price ?? 0));
                    leaveState = LeaveState.GoingToExit;
                }
                break;

            case LeaveState.GoingToExit:
                target = exitPositionTransform.position;
                MoveTowards(target);
                if (Vector2.Distance(rb.position, target) < 0.1f)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    public Food GetOrderedFood() => orderedFood;

    public bool IsSeated => hasSat && (!orderCompleted || isEating);

    public void CompleteOrder() => orderCompleted = true;
}































































