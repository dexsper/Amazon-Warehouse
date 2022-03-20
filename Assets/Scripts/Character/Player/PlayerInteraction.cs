using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PackageContainer))]
public class PlayerInteraction : MonoBehaviour
{
    private PackageContainer _container;

    public PackageContainer Container
    {
        get { return _container; }
    }

    private void Awake()
    {
        _container = GetComponent<PackageContainer>();
    }

    private IInteractable _currentInteraction;

    public IInteractable CurrentInteraction
    {
        get { return _currentInteraction; }
    }

    public void SetInteract(IInteractable interactable)
    {
        _currentInteraction = interactable;
    }

    public bool HasInteraction
    {
        get { return _currentInteraction != null; }
    }
}

