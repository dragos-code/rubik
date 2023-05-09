// A longer example of Vector3.Lerp usage.
// Drop this script under an object in your scene, and specify 2 other objects in the "startMarker"/"target" variables in the script inspector window.
// At play time, the script will move the object along a path between the position of those two markers.

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform centerCube;
    private float timeToMove = 1f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CheckTarget();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            
        }

        transform.LookAt(centerCube);
    }

    private void CheckTarget()
    {
        StartCoroutine(MoveCamera());
    }

    private IEnumerator MoveCamera()
    {
        float elapsedTime = 0;
        while (elapsedTime < timeToMove) 
        {
            transform.position = Vector3.Lerp(transform.position, target.position, elapsedTime/timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

}