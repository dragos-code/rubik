using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    RaycastManager raycastManager;
    ChildGroupingScript childGroupingScript;
    bool isInputPressed = false;
    private void Start()
    {
        raycastManager = FindObjectOfType<RaycastManager>();
        childGroupingScript = FindObjectOfType<ChildGroupingScript>();
    }
    void Update()
    {
        // Handle input from the player
        if (Input.GetKeyDown(KeyCode.F))
            if(!isInputPressed)
            {
                Debug.Log("GAMEmanar");

                isInputPressed = true;
                // Trigger the raycast from the corresponding position using the RaycastManager
                // Get the name of the collided game object
                string collidedObjectName = raycastManager.CastRaycasts("Up");

                // Assign the game object name as the stringToContain in the ChildGroupingScript
                childGroupingScript.stringToContain = collidedObjectName;

                // Group the matching children
                childGroupingScript.GroupMatchingChildren();

                // Create a RotateCommand instance and rotate the grouped objects
                RotateCommand rotateCommand = new RotateCommand(childGroupingScript.GetGroupObject(),
                    Quaternion.Euler(0, 90, 0));
                rotateCommand.Execute();

                // Ungroup the previously grouped objects
                childGroupingScript.UngroupMatchingChildren();
                

            }
        if(Input.GetKeyUp(KeyCode.F))
        {
            isInputPressed = false;
        }


    }

}
