using UnityEngine;

public class RaycastManager : MonoBehaviour
{
    public GameObject[] gameObjects;
    public Vector3[] directions;


    private void Update()
    {
        CastRaycasts();
    }
    public void CastRaycasts()
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(gameObjects[i].transform.position, directions[i], out hit))
            {
                string faceName = hit.collider.transform.name;
                Debug.Log("Hit parent name: " + faceName);
            }
            else
            {
                Debug.Log("no");
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (gameObjects == null || directions == null)
            return;

        Gizmos.color = Color.red;

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Gizmos.DrawRay(gameObjects[i].transform.position, directions[i]);
        }
    }
}
