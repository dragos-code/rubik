using UnityEngine;
using System.Collections.Generic;

public class ChildGroupingScript : MonoBehaviour
{
    public GameObject parentObject;
    public string stringToContain;
    private Transform groupObject;

    public Transform GetGroupObject() { return groupObject; }
    // Dictionary to store the original parent of each child
    private Dictionary<Transform, Transform> originalParents = new Dictionary<Transform, Transform>();

    private void Start()
    {
        // Create a new groupObject as a child of the parentObject
        groupObject = new GameObject("RotationPieces").transform;
        groupObject.SetParent(parentObject.transform);

        // Group the matching children
        //GroupMatchingChildren();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) { GroupMatchingChildren(); }
        if (Input.GetKeyDown(KeyCode.S)) { UngroupMatchingChildren(); }
    }

    public void GroupMatchingChildren()
    {
        int childCount = parentObject.transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = parentObject.transform.GetChild(i);

            if (child.name.Contains(stringToContain))
            {
                if (!originalParents.ContainsKey(child))
                {
                    // Store the original parent of the child
                    originalParents[child] = child.parent;
                }

                child.SetParent(groupObject);
            }
        }
    }

    public void UngroupMatchingChildren()
    {
        // Iterate through the grouped children in the groupObject
        int childCount = groupObject.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = groupObject.GetChild(i);

            // Check if the child was originally grouped
            if (originalParents.ContainsKey(child))
            {
                // Restore the original parent of the child
                Transform originalParent = originalParents[child];
                child.SetParent(originalParent);

                // Remove the child from the originalParents dictionary
                originalParents.Remove(child);
            }
        }
        groupObject.rotation = Quaternion.Euler(0, 0, 0) ;
    }

}
