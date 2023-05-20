using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public Transform[] startPos;
    public Vector3[] directions;

    string faceName;
    private void Update()
    {
     /*   if (Input.GetKeyDown(KeyCode.P))
        {
            if (!isRaycastTriggered)
            {
                CastRaycasts("Up");
                isRaycastTriggered = true;
            }
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            isRaycastTriggered = false;
        }*/
    }

    public string CastRaycasts(string directionName)
    {
      
        for (int i = 0; i < startPos.Length; i++)
        {
            if (startPos[i].name == directionName)
            {
                RaycastHit hit;
                if (Physics.Raycast(startPos[i].position, directions[i], out hit))
                {
                    faceName = hit.collider.gameObject.name;
                    
                    Debug.Log("Hit parent name: " + faceName);
                } 
               
            }
        }
        return faceName;
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
