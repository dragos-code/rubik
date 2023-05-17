using UnityEngine;

public class ChildGroupingScript : MonoBehaviour
{
    public GameObject parentObject;
    public string stringToContain;

    private Transform defaultParent;
    private GameObject groupObject;

    private void Start()
    {
        // Store the default parent and create a new group object
        defaultParent = parentObject.transform.parent;
        groupObject = new GameObject("GroupedChildren");
        groupObject.transform.SetParent(defaultParent);
        groupObject.transform.localPosition = Vector3.zero;

        // Group the matching children
        GroupMatchingChildren();
    }

    private void GroupMatchingChildren()
    {
        int childCount = parentObject.transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = parentObject.transform.GetChild(i);

            if (child.name.Contains(stringToContain))
            {
                child.SetParent(groupObject.transform);
            }
        }
    }

    public void RevertToDefault()
    {
        int childCount = groupObject.transform.childCount;

        for (int i = childCount - 1; i >= 0; i--)
        {
            Transform child = groupObject.transform.GetChild(i);
            child.SetParent(parentObject.transform);
        }

        Destroy(groupObject);
    }
}
