using System.Collections.Generic;
using UnityEngine;

public class GroupChildrenByDirection : MonoBehaviour
{
    public GameObject parentObject; // Reference to the parent GameObject
    public Vector3 direction; // Direction to group the children
    private Transform validChildrenObject; // Reference to the object grouping the valid children

    private Dictionary<Transform, Transform> originalParents = new Dictionary<Transform, Transform>();

    private void Start()
    {
        //ValidateDirection(direction);
        validChildrenObject = new GameObject("ValidChildren").transform; // Create a new game object to group the valid children
        validChildrenObject.transform.SetParent(parentObject.transform);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { ValidateDirection(direction); }
        if (Input.GetKeyDown(KeyCode.S)) { UnloadValidChildren(); }
    }
    public void ValidateDirection(Vector3 direction)
    {
        int nonZeroAxis = GetNonZeroAxis(direction);



        for (int i = parentObject.transform.childCount - 1; i >= 0; i--)
        {
            Transform child = parentObject.transform.GetChild(i);
            Vector3 childPosition = child.position;

            if (IsChildValid(childPosition, direction, nonZeroAxis))
            {
                if (!originalParents.ContainsKey(child))
                {
                    originalParents[child] = child.parent;
                }
                child.SetParent(validChildrenObject);
            }
        }
    }

    private int GetNonZeroAxis(Vector3 direction)
    {
        if (direction.x != 0)
        {
            return 0;
        }
        else if (direction.y != 0)
        {
            return 1;
        }
        else if (direction.z != 0)
        {
            return 2;
        }

        return -1; // Invalid direction
    }

    private bool IsChildValid(Vector3 childPosition, Vector3 direction, int nonZeroAxis)
    {
        Vector3 localChildPosition = parentObject.transform.InverseTransformPoint(childPosition);

        switch (nonZeroAxis)
        {
            case 0:
                return localChildPosition.x == direction.x;

            case 1:
                return localChildPosition.y == direction.y;

            case 2:
                return localChildPosition.z == direction.z;

            default:
                return false;
        }
    }

    public void UnloadValidChildren()
    {
        int childCount = validChildrenObject.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = validChildrenObject.GetChild(i);
            if (originalParents.ContainsKey(child))
            {
                Transform originalParent = originalParents[child];
                child.SetParent(originalParent);

                originalParents.Remove(child);
            }
        }
    }
}
