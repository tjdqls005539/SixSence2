using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class Draggable : MonoBehaviour
{
    private Vector3 _offset;
    private Vector3 _defaultPos;
    private Camera _mainCamera;
    public bool isOverTrash = false;

    void Awake()
    {
        _mainCamera = Camera.main;
    }
    void OnMouseDown()
    {
        _defaultPos = this.transform.position;
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _offset = transform.position - mouseWorldPos;
    }
    void OnMouseDrag()
    {
        Vector3 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition) + _offset;
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
    void OnMouseUp()
    {
        if (isOverTrash)
        {
            Destroy(gameObject);
        }
        else
        {
            this.transform.position = _defaultPos;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Trash")
        {
            isOverTrash = true;
        }
    }
}