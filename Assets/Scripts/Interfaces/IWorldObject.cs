using UnityEngine;

public interface IWorldObject : ITooltip
{
    void Interact();
    Vector3 Position { get; }
}

