using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Customer : MonoBehaviour
{
    [Header("이동 및 탐색 설정")]
    public float moveSpeed = 2f;
    public LayerMask obstacleLayer;

    [Header("주문 관련 설정")]
    public Food[] foodOptions;

    [Header("접시 프리팹 연결")]
    public GameObject dishPrefab;

    [Header("참조")]
    public MoneyManager moneyManager;

    [Header("카운터 및 출구 설정")]
    public Transform counterPosition;
    public Transform exitPositionTransform;

    private GameObject orderIconGO;
    private SpriteRenderer orderIconRenderer;
    private Rigidbody2D rb;
    private CC targetChair;
    private Vector2 targetPosition;
    private bool hasChair = false;
    private bool isLeaving = false;
    private bool orderCompleted = false;
    private bool hasPaid = false;
    private Food orderedFood;

    private enum LeaveState { None, GoingToCounter, PayingAtCounter, GoingToExit }
    private LeaveState leaveState = LeaveState.None;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    public void StartFindingChair()
    {
        StartCoroutine(FindChairRoutine());
    }

    private IEnumerator FindChairRoutine()
    {
        while (!hasChair)
        {
            CC chair = CCManager.Instance.GetAvailableChair();
            if (chair != null && chair.Reserve())
            {
                targetChair = chair;
                targetPosition = chair.transform.position;
                break;
            }
            yield return new WaitForSecondsRealtime(0.2f);
        }
    }

    void Update()
    {
        if (!hasChair && !isLeaving && targetChair != null)
        {
            Vector2 moveTarget = targetPosition + Random.insideUnitCircle * 0.05f;
            MoveTowardsPosition(moveTarget);
        }
        else if (isLeaving)
        {
            HandleLeaving();
        }
    }

    private void HandleLeaving()
    {
        if (counterPosition == null || exitPositionTransform == null)
            return;

        Vector2 targetPos = Vector2.zero;

        switch (leaveState)
        {
            case LeaveState.GoingToCounter:
                targetPos = counterPosition.position;
                if (Vector2.Distance(rb.position, targetPos) < 0.1f)
                {
                    leaveState = LeaveState.PayingAtCounter;
                }
                break;

            case LeaveState.PayingAtCounter:
                if (!hasPaid)
                {
                    hasPaid = true;
                    if (orderedFood != null && moneyManager != null)
                    {
                        moneyManager.AddMoney((int)orderedFood.price);
                        Debug.Log($"{gameObject.name} 손님이 Counter에서 {orderedFood.price}원을 결제했습니다!");
                    }
                }
                leaveState = LeaveState.GoingToExit;
                break;

            case LeaveState.GoingToExit:
                targetPos = exitPositionTransform.position;
                if (Vector2.Distance(rb.position, targetPos) < 0.1f)
                {
                    Destroy(gameObject);
                    return;
                }
                break;
        }

        if (leaveState != LeaveState.PayingAtCounter)
            MoveTowardsPosition(targetPos);
    }

    private void MoveTowardsPosition(Vector2 targetPos)
    {
        Vector2 moveDir = targetPos - rb.position;
        float distance = moveDir.magnitude;

        if (distance > 0.01f)
        {
            moveDir.Normalize();

            Collider2D[] obstacles = Physics2D.OverlapCircleAll(rb.position, 0.7f, obstacleLayer);
            foreach (Collider2D obstacle in obstacles)
            {
                Vector2 awayFromObstacle = rb.position - (Vector2)obstacle.transform.position;
                float weight = 1f / Mathf.Max(awayFromObstacle.magnitude, 0.1f);
                moveDir += awayFromObstacle.normalized * weight;
            }

            moveDir.Normalize();
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);

            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            rb.rotation = angle - 90f;
        }

        if (!hasChair && !isLeaving && Vector2.Distance(rb.position, targetPosition) < 0.1f)
            SitOnChair();
    }

    private void SitOnChair()
    {
        if (targetChair == null)
        {
            Destroy(gameObject);
            return;
        }

        hasChair = true;
        targetChair.Sit();
        rb.position = targetChair.transform.position;

        float chairAngle = Mathf.Atan2(targetChair.sitDirection.y, targetChair.sitDirection.x) * Mathf.Rad2Deg;
        rb.rotation = chairAngle - 90f;

        if (foodOptions != null && foodOptions.Length > 0)
        {
            int index = Random.Range(0, foodOptions.Length);
            orderedFood = foodOptions[index];
            if (OrderManager.Instance != null)
                OrderManager.Instance.AddOrder(orderedFood);

            Debug.Log($"{gameObject.name} 손님이 {orderedFood.foodName}을 주문했습니다. (가격: {orderedFood.price}원)");
        }

        if (orderedFood != null && orderedFood.icon != null)
        {
            orderIconGO = new GameObject("MainHall_Order");
            orderIconGO.transform.SetParent(transform);
            orderIconGO.transform.localPosition = Vector3.up * 1.5f;
            orderIconGO.transform.localScale = Vector3.one * 0.5f;

            orderIconRenderer = orderIconGO.AddComponent<SpriteRenderer>();
            orderIconRenderer.sprite = orderedFood.icon;
            orderIconRenderer.sortingOrder = 10;
        }

        StartCoroutine(WaitForOrderRoutine());
    }

    private IEnumerator WaitForOrderRoutine()
    {
        while (!orderCompleted)
            yield return null;

        if (orderIconGO != null)
        {
            Destroy(orderIconGO);
            orderIconGO = null;
        }

        LeaveChair();
    }

    public Food GetOrderedFood() => orderedFood;

    public void ReceiveFood(GameObject deliveredFood = null)
    {
        if (orderCompleted) return;

        orderCompleted = true;

        if (orderIconGO != null)
        {
            Destroy(orderIconGO);
            orderIconGO = null;
        }

        if (deliveredFood != null)
            Destroy(deliveredFood);

        LeaveChair();
    }

    public void CompleteOrder() => ReceiveFood();

    public void LeaveChair()
    {
        if (targetChair != null)
        {
            if (dishPrefab != null)
            {
                GameObject dish = Instantiate(dishPrefab, targetChair.transform.position, Quaternion.identity);
                Dish dishScript = dish.GetComponent<Dish>();
                if (dishScript != null)
                    dishScript.linkedChair = targetChair;

                targetChair.isOccupied = true;
            }

            targetChair.Leave();
        }

        targetChair = null;
        hasChair = false;
        isLeaving = true;
        leaveState = LeaveState.GoingToCounter; // 먼저 카운터로 이동
    }

    public void SetExit(Transform exitTransform)
    {
        exitPositionTransform = exitTransform;
    }

    public bool IsSeated => hasChair && !isLeaving && !orderCompleted;
}




















































