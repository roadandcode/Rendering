using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    //public Transform lookAtTarget;
    public float zoomSpeed = 5f;
    public float yMoveSpeed = 2f;
    private bool isMoving = false;
    private Vector3 lastMousePosition;

    private void Update()
    {

        HandleZoom();
        HandleYPos();

        //var xClamp = Mathf.Clamp(transform.position.x, 1, 6);
        var yClamp = Mathf.Clamp(transform.position.y, 0.5f, 2);
        var zClamp = Mathf.Clamp(transform.position.z, -5, 0);

        Vector3 clampPos = new Vector3(0, yClamp, zClamp);

        transform.position = clampPos;


    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            transform.localPosition += transform.forward * scroll * zoomSpeed;
        }
    }

    void HandleYPos()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Vector3 deltaMouse = (Input.mousePosition - lastMousePosition).normalized;
            float yMoveAmount = deltaMouse.y * yMoveSpeed * Time.deltaTime;

            transform.localPosition += transform.up * yMoveAmount * -1;
            lastMousePosition = Input.mousePosition;
        }
    }
}
