using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerInteraction : PackageContainer
{
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

    public bool HasPackage(PackageState state)
    {
        return (_packages.Count > 0 && _packages.ElementAt(0).State == state) || _packages.Count == 0;
    }
}

