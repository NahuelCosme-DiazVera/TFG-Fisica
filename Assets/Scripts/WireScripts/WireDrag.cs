using UnityEngine;
public class WireDrag : MonoBehaviour
{
    public MagneticFieldVisualizer visualizer;
    private Camera mainCamera;
    public int wireIndex = -1;

    private bool isDragging;
    public bool isDoubleWire;
    private Plane dragPlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        dragPlane = new Plane(Vector3.forward, Vector3.zero);

        if (isDoubleWire && wireIndex < 0 && transform.parent != null)
        {
            wireIndex = transform.GetSiblingIndex();
        }
    }

    void OnMouseDown()
    {
        isDragging = true;
        visualizer.SelectWire(wireIndex, this.gameObject);
    }

    void OnMouseUp()
    {
        isDragging = false;
        if (isDoubleWire) {
            Vector3 newPosition = transform.position;
            float newX = newPosition.x;
            float newY = newPosition.y;
            visualizer.SetDoubleWirePosition(wireIndex, newX, newY, true);
        }
    }

    void Update() 
    {
        if (isDragging)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                float newX = hitPoint.x;
                float newY = hitPoint.y;
                if (!isDoubleWire)
                {
                    visualizer.SetWirePositionX(newX);
                    visualizer.SetWirePositionY(newY);
                }
                else
                {
                    visualizer.SetDoubleWirePosition(wireIndex, newX, newY, false);
                }
            }
        }
    }
}
