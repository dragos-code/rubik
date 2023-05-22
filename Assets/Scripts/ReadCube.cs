using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{

    public Transform RayUp;
    public Transform RayDown;
    public Transform RayFront;
    public Transform RayBack;
    public Transform RayLeft;
    public Transform RayRight;

    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();

    private int layerMask = 1 << 6;

    CubeState cubeState;

    public GameObject emptyGameObject;
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        SetRayTransforms();
        //ReadState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadState()
    {
        cubeState.up = ReadFace(upRays, RayUp);
        cubeState.down = ReadFace(downRays, RayDown);
        cubeState.right = ReadFace(rightRays, RayRight);
        cubeState.left = ReadFace(leftRays, RayLeft);
        cubeState.front = ReadFace(frontRays, RayFront);
        cubeState.back = ReadFace(backRays, RayBack);
    }

    void SetRayTransforms()
    {
        upRays = BuildRays(RayUp, new Vector3(90, 0, 0));
        downRays = BuildRays(RayDown, new Vector3(270, 90, 0));
        frontRays = BuildRays(RayFront, new Vector3(0, 0, 0));
        backRays = BuildRays(RayBack, new Vector3(0, 180, 0));
        leftRays = BuildRays(RayLeft, new Vector3(0, 90, 0));
        rightRays = BuildRays(RayRight, new Vector3(0, 270, 0));
    }
    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();

        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x, rayTransform.localPosition.y + y, rayTransform.localPosition.z);

                GameObject rayStart = Instantiate(emptyGameObject, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;

            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;

    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();
        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.red);

            }
        }
        return facesHit;
    }
}


