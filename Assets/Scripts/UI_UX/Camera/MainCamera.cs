using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [Header("REQUIRED: Camera Movement Limits")] 
    [SerializeField] private float x_Limit;
    [SerializeField] private float y_Limit;
    
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;
    
    private Vector3 dragOrigin;
    private Camera cam;

    void Awake()
    {
        minX = -x_Limit;
        maxX = x_Limit;
        minY = -y_Limit;
        maxY = y_Limit;
    }
    
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        HandleMouseDrag();
    }

    void LateUpdate()
    {
        ClampCamera();
    }

    private void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            transform.position += difference;
        }
    }

    private void ClampCamera()
    {
        Vector3 viewPos = transform.position;
        
        viewPos.x = Mathf.Clamp(viewPos.x, minX, maxX);
        viewPos.y = Mathf.Clamp(viewPos.y, minY, maxY);
        
        transform.position = viewPos;
    }

    private void ResetCamera()
    {
        transform.position = Vector2.zero;
    }
}
