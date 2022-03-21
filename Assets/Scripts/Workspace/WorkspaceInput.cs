using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PackageContainer))]
public class WorkspaceInput : BaseInteraction
{
    private PackageContainer _container;

    public InteractionType InteractType => InteractionType.Table;

    public PackageContainer Container => _container;

    protected void Awake()
    {
        _container = GetComponent<PackageContainer>();
    }

    public override bool CanInteract()
    {
        if (_player.Interaction.HasInteraction && _player.Interaction.CurrentInteraction != (this as IInteractable)) return false;

        if (_container.Equipped) return false;
        if (_player.Interaction.Container.HasPackages == false) return false;

        if (_player.Interaction.Container.PackagesCount > 0 &&
            _player.Interaction.Container.HasPackage(PackageState.New) == false) return false;

        return true;
    }

    public override void Interact()
    {
        if (!CanInteract()) return;

        var package = _player.Interaction.Container.GetPackage();

        _container.AddPackage(package);

    }
}
