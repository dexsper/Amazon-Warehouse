using UnityEngine;
using Zenject;

[RequireComponent(typeof(PackageContainer))]
public class ConveyorOutput : BaseInteraction
{
    private PackageContainer _container;

    public InteractionType InteractType => InteractionType.Conveyor;
    public PackageContainer Container => _container;

    protected void Awake()
    {
        _container = GetComponent<PackageContainer>();
    }
    public override bool CanInteract()
    {
        if (_player.Interaction.HasInteraction && _player.Interaction.CurrentInteraction != (this as IInteractable)) return false;

        if (_container.HasPackages == false) return false;
        if (_player.Interaction.Container.PackagesCount > 0 &&
            _player.Interaction.Container.HasPackage(PackageState.Sorted) == false) return false;


        return true;
    }
    public override void Interact()
    {
        if (!CanInteract()) return;

        var package = _container.GetPackage();

        _player.Interaction.Container.AddPackage(package);

    }
}