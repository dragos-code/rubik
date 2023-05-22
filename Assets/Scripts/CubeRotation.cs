using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private bool isRotating = false;
    private bool isFaceRotating = false;

    private ChildGroupingScript childGroupingScript;

    private void Start()
    {
        childGroupingScript = FindObjectOfType<ChildGroupingScript>();
    }

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

       /*     if(Input.GetKeyDown(KeyCode.Q)) { RotateFace(Vector3.up, childGroupingScript.GetGroupObject()); }
            if(Input.GetKeyDown(KeyCode.W)) { RotateFace(Vector3.up, childGroupingScript.GetGroupObject()); }
            if(Input.GetKeyDown(KeyCode.E)) { RotateFace(Vector3.up, childGroupingScript.GetGroupObject()); }
            if(Input.GetKeyDown(KeyCode.R)) { RotateFace(Vector3.up, childGroupingScript.GetGroupObject()); }
            if(Input.GetKeyDown(KeyCode.T)) { RotateFace(Vector3.up, childGroupingScript.GetGroupObject()); }
            if(Input.GetKeyDown(KeyCode.Y)) { RotateFace(Vector3.up, childGroupingScript.GetGroupObject()); }*/
        }
    }

    private void RotateCube(Vector3 axis)
    {
        targetRotation = Quaternion.Euler(axis * 90f) * transform.rotation;  // gargantuan kraken eldrich solution
        isRotating = true;
    }

    private void RotateFace(Vector3 axis, Transform target)
    {
        childGroupingScript.GroupMatchingChildren();
        targetRotation = Quaternion.Euler(axis * 90f) * target.rotation;  // gargantuan kraken eldrich solution
        isFaceRotating = true;
    }

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * Time.fixedDeltaTime);
            if(Quaternion.Angle(transform.rotation, targetRotation) <=1)
            {
                transform.rotation = targetRotation;
                isRotating = false;

            }
            /*if (transform.rotation == targetRotation)
            {
            }*/
        }

        if(isFaceRotating) 
        {
            childGroupingScript.GetGroupObject().rotation = Quaternion.RotateTowards(childGroupingScript.GetGroupObject().rotation, targetRotation, 180f* Time.fixedDeltaTime);
            if (childGroupingScript.GetGroupObject().rotation == targetRotation)
            {
                isFaceRotating = false;
                childGroupingScript.UngroupMatchingChildren();
            }
        }
    }
}