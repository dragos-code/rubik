using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private bool isRotating = false;

    private void Update()
    {
        if (!isRotating)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateCube(Vector3.right);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                RotateCube(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RotateCube(Vector3.up);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RotateCube(Vector3.down);
            }
        }
    }

    private void RotateCube(Vector3 axis)
    {
        targetRotation = Quaternion.Euler(axis * 90f) * transform.rotation;
        isRotating = true;
    }

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * Time.fixedDeltaTime);

            if (transform.rotation == targetRotation)
            {
                isRotating = false;
            }
        }
    }
}