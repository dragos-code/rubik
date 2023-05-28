using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Quaternion targetQuaternion;
    private float speed = 300f;
    private ReadCube readCube;
    private CubeState cubeState;

    private bool isRotating;

    public bool IsRotating { get { return isRotating; } }
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    private void LateUpdate()
    {
        if (isRotating)
        {
            AutoRotate();
        }
    }

    public void Rotate(List<GameObject> sides)
    {
        activeSide = sides;
        localForward = Vector3.zero - sides[4].transform.parent.transform.localPosition;
    }

    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.GroupFacePieces(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        isRotating = true;
    }

    private void AutoRotate()
    {
        var step = speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            cubeState.UngroupFacePieces(activeSide, transform.parent);
            readCube.ReadState();
            isRotating = false;
        }
    }
}
