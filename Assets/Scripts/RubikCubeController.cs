using UnityEngine;

public class RubikCubeController : MonoBehaviour
{
    private Quaternion initialRotation;
    private Quaternion currentRotation;
    private bool isRotating = false;

    private Vector3 initialTouchPosition;

    public float snapThreshold = 10f; // Adjust this value to control the snap sensitivity

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            initialTouchPosition = Input.mousePosition;
            initialRotation = transform.rotation;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            currentRotation = transform.rotation;

            // Check if the rotation is misaligned with the most visible face
            if (!IsRotationAligned(currentRotation))
            {
                // Snap the rotation to the nearest aligned state
                Quaternion snappedRotation = SnapToAlignedRotation(currentRotation);
                transform.rotation = snappedRotation;
                currentRotation = snappedRotation;
            }
        }

        if (isRotating)
        {
            Vector3 deltaTouchPosition = Input.mousePosition - initialTouchPosition;
            Quaternion rotation = Quaternion.Euler(deltaTouchPosition.y, -deltaTouchPosition.x, 0f);
            transform.rotation = initialRotation * rotation;
        }
        else
        {
            transform.rotation = currentRotation;
        }
    }

    bool IsRotationAligned(Quaternion rotation)
    {
        Vector3 forward = transform.forward;
        Vector3 cameraForward = Camera.main.transform.forward;
        float angle = Vector3.Angle(forward, cameraForward);
        return Mathf.Abs(angle) < snapThreshold;
    }

    Quaternion SnapToAlignedRotation(Quaternion rotation)
    {
        Vector3 forward = transform.forward;
        Vector3 cameraForward = Camera.main.transform.forward;

        // Calculate the target rotation by aligning the most visible face with the camera
        Quaternion targetRotation = Quaternion.FromToRotation(GetMostVisibleFaceNormal(), cameraForward) * rotation;

        // Snap the target rotation to a 90-degree increment
        Vector3 eulerAngles = targetRotation.eulerAngles;
        float x = SnapAngle(eulerAngles.x);
        float y = SnapAngle(eulerAngles.y);
        float z = SnapAngle(eulerAngles.z);
        targetRotation = Quaternion.Euler(x, y, z);

        return targetRotation;
    }

    Vector3 GetMostVisibleFaceNormal()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3[] faceNormals = {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right,
            Vector3.forward,
            Vector3.back
        };

        Vector3 mostVisibleNormal = Vector3.zero;
        float maxDot = -Mathf.Infinity;

        foreach (Vector3 normal in faceNormals)
        {
            float dot = Vector3.Dot(normal, cameraPosition - transform.position);
            float absDot = Mathf.Abs(dot);
            if (absDot > maxDot)
            {
                maxDot = absDot;
                mostVisibleNormal = normal;
            }
        }

        return mostVisibleNormal;
    }

    float SnapAngle(float angle)
    {
        float snappedAngle = Mathf.Round(angle / 90f) * 90f;
        return snappedAngle;
    }
}
