using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    public List<GameObject> front = new List<GameObject>();
    public List<GameObject> back = new List<GameObject>();
    public List<GameObject> up = new List<GameObject>(); 
    public List<GameObject> down = new List<GameObject>(); 
    public List<GameObject> left = new List<GameObject>(); 
    public List<GameObject> right = new List<GameObject>(); 

    public void GroupFacePieces(List<GameObject> cubeSide)
    {
        foreach (GameObject face in cubeSide)
        {
            if (face != cubeSide[4])
            {
                face.transform.parent.transform.parent = cubeSide[4].transform.parent;
            }
        }
        cubeSide[4].transform.parent.GetComponent<PivotRotation>().Rotate(cubeSide);
    }

    public void UngroupFacePieces(List<GameObject> cubelets, Transform pivot)
    {
        foreach(GameObject face in cubelets)
        {
            if (face!= cubelets[4])
            {
                face.transform.parent.transform.parent = cubelets[4].transform.parent.transform.parent;
            }
        }
    }

}
