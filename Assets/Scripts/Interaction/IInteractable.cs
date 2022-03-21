using UnityEngine;

public enum InteractionType
{
    Truck,
    Table,
    Conveyor
}

public interface IInteractable
{
    public void Interact();

    public bool CanInteract();

    public InteractionType InteractType { get; }
}

