using UnityEngine;
using UnityEngine.InputSystem;

public class FollowMouse : MonoBehaviour {
    public static FollowMouse Instance;

    private Camera _mainCamera;
    [Header("Boundary Settings")]
    [Tooltip("Drag a GameObject with Collider2D here")]
    [SerializeField] private Collider2D boundaryCollider;
    private BoxCollider2D _objectCollider;
    private float _minX, _maxX, _minY, _maxY;

    private Rigidbody2D _rb;

    private void Awake()
    {
        Instance = this;
        _objectCollider = GetComponent<BoxCollider2D>();
        _mainCamera = Camera.main;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Bounds bounds = boundaryCollider.bounds;
        Bounds objBounds = _objectCollider.bounds;

        _minX = bounds.min.x + objBounds.extents.x;
        _maxX = bounds.max.x - objBounds.extents.x;
        _minY = bounds.min.y + objBounds.extents.y;
        _maxY = bounds.max.y - objBounds.extents.y;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GetIsGameRunning() == true) {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = 5.0f;
            Vector3 worldPos = _mainCamera.ScreenToWorldPoint(mousePos);
            worldPos.x = Mathf.Clamp(worldPos.x, _minX, _maxX);
            worldPos.y = Mathf.Clamp(worldPos.y, _minY, _maxY);

            _rb.MovePosition(worldPos);
        }
    }

    public Vector2 GetPositionPlatform()
    {
        return gameObject.transform.position;
    }
}