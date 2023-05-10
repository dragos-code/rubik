using UnityEngine;

public class ChangePivot : MonoBehaviour
{
    public Quaternion newPivotRotation; // The new desired pivot rotation
    public Vector3 newPivot; // The new desired pivot point

    void Start()
    {
        // Calculate the rotation difference between the old pivot and the new pivot
        Quaternion rotationOffset = newPivotRotation * Quaternion.Inverse(transform.rotation);
        Vector3 offset = newPivot - transform.position;


        // Adjust child rotations relative to the new pivot
        foreach (Transform child in transform)
        {
            child.rotation *= rotationOffset;
            child.position -= offset;
        }

        // Update the pivot rotation of the parent
        transform.rotation = newPivotRotation;
        transform.position = newPivot;
    }
}
