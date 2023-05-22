using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public Transform[] startPos;
    public Vector3[] directions;

    Vector3 faceIndex;
    private void Update()
    {
        CastRaycasts("Up");
    }

    public Vector3 CastRaycasts(string directionName)
    {
      
        for (int i = 0; i < startPos.Length; i++)
        {
            if (startPos[i].name == directionName)
            {
                RaycastHit hit;
                if (Physics.Raycast(startPos[i].position, directions[i], out hit))
                {
                    //faceName = hit.collider.gameObject.name;
                    faceIndex = hit.collider.gameObject.transform.position;
                    //Debug.Log("Hit parent name: " + faceIndex );
                } 
               
            }
        }
        return faceIndex;
    }

    private void OnDrawGizmos()
    {
        if (startPos == null || directions == null)
            return;

        Gizmos.color = Color.red;

        for (int i = 0; i < startPos.Length; i++)
        {
            Gizmos.DrawRay(startPos[i].transform.position, directions[i]);
        }
    }
}
