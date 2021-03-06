using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PackageContainer))]
public class ConveyorInput : BaseInteraction
{
    private PackageContainer _container;

    public InteractionType InteractType => InteractionType.Conveyor;
    public PackageContainer Container => _container;

    protected override void Awake()
    {
        base.Awake();

        _container = GetComponent<PackageContainer>();
    }
    public override bool CanInteract()
    {
        if (_player.Interaction.HasInteraction && _player.Interaction.CurrentInteraction != (this as IInteractable)) return false;

        if (_container.Equipped) return false;
        if(_player.Interaction.Container.HasPackages == false) return false;

        if (_player.Interaction.Container.PackagesCount > 0 && 
            _player.Interaction.Container.HasPackage(PackageState.Calculated) == false) return false;

        return true;
    }
    public override void Interact()
    {
        if (!CanInteract()) return;

        var package = _player.Interaction.Container.GetPackage();

        _container.AddPackage(package);

    }
}
