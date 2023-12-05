using UnityEngine;

public class RoomRotation : MonoBehaviour
{
    public float rotationSpeed = 2f;

    private bool isRotating = false;
    private Vector3 lastMousePosition;

    void Update()
    {
        HandleRotationInput();
    }

    void HandleRotationInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        if (isRotating)
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationAmount = deltaMouse.x * rotationSpeed * Time.deltaTime;

            transform.Rotate(Vector3.down, rotationAmount);

            lastMousePosition = Input.mousePosition;
        }
    }
}
