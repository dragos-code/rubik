using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.InspectorCurveEditor;

public class PivotRotation : MonoBehaviour
{

    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Quaternion targetQuaternion;
    public bool autoRotating;
    private float speed = 300f;


    private ReadCube readCube;
    private CubeState cubeState;

    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    private void LateUpdate()
    {
        if (autoRotating)
        {
            AutoRotate();
        }
    }
    // Start is called before the first frame update
    public void Rotate(List<GameObject> sides)
    {
        activeSide = sides;
        localForward = Vector3.zero - sides[4].transform.parent.transform.localPosition;
    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        // round vec to nearest 90 degrees
        vec.x = Mathf.Round(vec.x / 90) * 90;
        vec.y = Mathf.Round(vec.y / 90) * 90;
        vec.z = Mathf.Round(vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;
    }


    public void StartAutoRotate(List<GameObject> side, float angle)
    {
        cubeState.GroupFacePieces(side);
        Vector3 localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
        targetQuaternion = Quaternion.AngleAxis(angle, localForward) * transform.localRotation;
        activeSide = side;
        autoRotating = true;
    }

    private void AutoRotate()
    {
        var step = speed * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        // if within one degree, set angle to target angle and end the rotation
        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            // unparent the little cubes
            cubeState.UngroupFacePieces(activeSide, transform.parent);
            readCube.ReadState();
            //CubeState.autoRotating = false;
            autoRotating = false;
        }
    }
}
