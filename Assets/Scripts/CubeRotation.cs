using System;
using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private Quaternion currentRotation;
    private bool isRotating = false;
    private Vector3 initialMousePosition;
    public float snapThreshold =10;

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

        if (Input.GetMouseButtonDown(0))
        {
            initialMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentRotation = transform.rotation;
            isRotating = false;

            if (!IsRotationAligned(currentRotation))
            {
                Quaternion snappedRotation = SnapToAlignedRotation(currentRotation);
                transform.rotation = snappedRotation;
                currentRotation = snappedRotation;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            RotateCubeWithDrag();
        }
    }

    private Quaternion SnapToAlignedRotation(Quaternion rotation)
    {
        Vector3 forward = transform.forward;
        Vector3 cameraForward = Camera.main.transform.forward;

        Quaternion targetRotation = Quaternion.FromToRotation(GetMostVisibleFaceNormal(), cameraForward) * rotation;

        Vector3 eulerAngles = targetRotation.eulerAngles;
        float x = SnapAngle(eulerAngles.x);
        float y = SnapAngle(eulerAngles.y);
        float z = SnapAngle(eulerAngles.z);
        targetRotation = Quaternion.Euler(x, y, z);

        return targetRotation;
    }

    private float SnapAngle(float angle)
    {
        float snappedAngle = Mathf.Round(angle / 90f) * 90f;
        return snappedAngle;
    }

    private Vector3 GetMostVisibleFaceNormal()
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

    private bool IsRotationAligned(Quaternion currentRotation)
    {
        Vector3 forward = transform.forward;
        Vector3 cameraForward = Camera.main.transform.forward;
        float angle = Vector3.Angle(forward, cameraForward);
        return Mathf.Abs(angle) < snapThreshold;
    }

    private void RotateCube(Vector3 axis)
    {
        targetRotation = Quaternion.Euler(axis * 90f) * transform.rotation;  // gargantuan kraken eldrich solution
        isRotating = true;
    }

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * Time.fixedDeltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) <= 1)
            {
                transform.rotation = targetRotation;
                isRotating = false;

            }
        }

    }

    private void RotateCubeWithDrag()
    {
        Vector3 dragDelta = Input.mousePosition - initialMousePosition;
        float dragAngleX = dragDelta.y * 0.5f;
        float dragAngleY = -dragDelta.x * 0.5f;
        float clampedAngleX = Mathf.Clamp(dragAngleX, -45f, 45f);
        float clampedAngleY = Mathf.Clamp(dragAngleY, -45f, 45f);

        Quaternion rotationDelta = Quaternion.Euler(clampedAngleX, clampedAngleY, 0f);

        targetRotation = rotationDelta * transform.rotation;
        initialMousePosition = Input.mousePosition;
        isRotating = true;
    }
}