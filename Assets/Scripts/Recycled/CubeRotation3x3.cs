//// A longer example of Vector3.Lerp usage.
//// Drop this script under an object in your scene, and specify 2 other objects in the "startMarker"/"target" variables in the script inspector window.
//// At play time, the script will move the object along a path between the position of those two markers.
using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class CubeRotation3x3 : MonoBehaviour
{
    private float timeToMove = 2f;
    private Quaternion wishRotation;
    private Quaternion originRot;
    private const float ROTATION_ANGLE = 90f;
    private bool isMoving = false;

    [SerializeField] Camera mainCamera;

    void Update()
    {

        if (!isMoving)
        {
            originRot = transform.rotation;
        }
        Debug.Log(isMoving.ToString());
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isMoving)
        {
            StartCoroutine(RotateCube(mainCamera.transform.right));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving)
        {

            StartCoroutine(RotateCube(-mainCamera.transform.right));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && !isMoving)
        {

            StartCoroutine(RotateCube(mainCamera.transform.up));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isMoving)
        {
            StartCoroutine(RotateCube(-mainCamera.transform.up));
        }
    }

    private IEnumerator RotateCube(Vector3 axis)
    {
        isMoving = true;
        float elapsedTime = 0;
        Vector3 worldAxis = transform.TransformDirection(axis);

        wishRotation = originRot * Quaternion.Euler(ROTATION_ANGLE * axis);
        while (elapsedTime < timeToMove)
        {
            transform.rotation = Quaternion.Slerp(originRot, wishRotation, elapsedTime / timeToMove);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = wishRotation;
        originRot = wishRotation;
        isMoving = false;
    }

}