using System.Collections.Generic;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    PivotRotation pivotRotation;

    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        pivotRotation = FindObjectOfType<PivotRotation>();
    }

    void Update()
    {
        if (!pivotRotation.GetRotationStatus())
        {
            readCube.ReadState();

            if (Input.GetKeyDown(KeyCode.Q))
                RotateFace(cubeState.front, -90);
            else if (Input.GetKeyDown(KeyCode.W))
                RotateFace(cubeState.back, -90);
            else if (Input.GetKeyDown(KeyCode.E))
                RotateFace(cubeState.up, -90);
            else if (Input.GetKeyDown(KeyCode.R))
                RotateFace(cubeState.down, -90);
            else if (Input.GetKeyDown(KeyCode.T))
                RotateFace(cubeState.left, -90);
            else if (Input.GetKeyDown(KeyCode.Y))
                RotateFace(cubeState.right, -90);
            else if (Input.GetKeyDown(KeyCode.A))
                RotateFace(cubeState.front, 90);
            else if (Input.GetKeyDown(KeyCode.S))
                RotateFace(cubeState.back, 90);
            else if (Input.GetKeyDown(KeyCode.D))
                RotateFace(cubeState.up, 90);
            else if (Input.GetKeyDown(KeyCode.F))
                RotateFace(cubeState.down, 90);
            else if (Input.GetKeyDown(KeyCode.G))
                RotateFace(cubeState.left, 90);
            else if (Input.GetKeyDown(KeyCode.H))
                RotateFace(cubeState.right, 90);
        }
    }

    void RotateFace(List<GameObject> face, int angle)
    {
        pivotRotation = face[4].transform.parent.GetComponent<PivotRotation>();
        pivotRotation.StartAutoRotate(face, angle);
    }
}
