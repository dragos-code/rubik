using UnityEngine;

public class RotateCommand : ICommand
{
    private Transform _target;
    private Quaternion _previousRotation;
    private Quaternion _newRotation;

    public RotateCommand(Transform target, Quaternion newRotation)
    {
        _target = target;
        _newRotation = newRotation;
    }

    public void Execute()
    {
        _previousRotation = _target.rotation;
        _target.rotation = _newRotation;
    }

    public void Undo()
    {
        _target.rotation = _previousRotation;
    }
}