using UnityEngine;

public class CubeRotation : MonoBehaviour
{
    private Quaternion targetRotation;
    private bool isRotating = false;

    private CommandManager commandManager;

    private void Start()
    {
        commandManager = FindObjectOfType<CommandManager>();
    }

    private void Update()
    {
        if (!isRotating)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                RotateCube(Vector3.right);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                RotateCube(Vector3.left);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                RotateCube(Vector3.up);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                RotateCube(Vector3.down);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Quaternion newRotation = transform.rotation * Quaternion.Euler(0f, 45f, 0f);
                ICommand rotateCommand = new RotateCommand(transform, newRotation);
                commandManager.ExecuteCommand(rotateCommand);
            }

            if (Input.GetKeyDown(KeyCode.U))
            {
                commandManager.UndoLastCommand();
            }
        }
    }

    private void RotateCube(Vector3 axis)
    {
        targetRotation = Quaternion.Euler(axis * 90f) * transform.rotation;  // gargantuan kraken eldrich solution
        isRotating = true;
    }

    private void FixedUpdate()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180f * Time.fixedDeltaTime);

            if (transform.rotation == targetRotation)
            {
                isRotating = false;
            }
        }
    }
}