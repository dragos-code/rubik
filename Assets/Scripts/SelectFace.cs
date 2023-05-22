using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectFace : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;
    PivotRotation pivotRotation;

    //int layerMask = 1 << 6;
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
        pivotRotation = FindObjectOfType<PivotRotation>();
    }

    // Update is called once per frame
    void Update()
    {

        // F
        if(Input.GetKeyDown(KeyCode.Q) && !pivotRotation.autoRotating ) 
        {
            readCube.ReadState();

            pivotRotation = cubeState.front[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.front, -90);
        }

        // B
        if (Input.GetKeyDown(KeyCode.W) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.back[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.back, -90);
        }

        //U
        if (Input.GetKeyDown(KeyCode.E) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.up[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.up, -90);
        }

        //D
        if (Input.GetKeyDown(KeyCode.R) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.down[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.down, -90);
        }

        //L
        if (Input.GetKeyDown(KeyCode.T) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.left[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.left, -90);
        }

        //R
        if (Input.GetKeyDown(KeyCode.Y) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.right[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.right, -90);
        }



        // F'
        if (Input.GetKeyDown(KeyCode.A) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.front[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.front, 90);
        }

        // B'
        if (Input.GetKeyDown(KeyCode.S) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.back[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.back, 90);
        }

        //U'
        if (Input.GetKeyDown(KeyCode.D) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.up[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.up, 90);
        }

        //D'
        if (Input.GetKeyDown(KeyCode.F) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.down[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.down, 90);
        }

        //L'
        if (Input.GetKeyDown(KeyCode.G) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.left[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.left, 90);
        }

        //R'
        if (Input.GetKeyDown(KeyCode.H) && !pivotRotation.autoRotating)
        {
            readCube.ReadState();

            pivotRotation = cubeState.right[4].transform.parent.GetComponent<PivotRotation>();
            pivotRotation.StartAutoRotate(cubeState.right, 90);
        }
    }

   
}
