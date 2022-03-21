using UnityEngine;

[RequireComponent(typeof(PackageContainer))]
public class WorkspaceOutput : BaseInteraction
{
    private PackageContainer _container;

    public InteractionType InteractType => InteractionType.Table;

    public PackageContainer Container => _container;

    private Workspace _workspace = null;

    protected override void Awake()
    {
        base.Awake();

        _container = GetComponent<PackageContainer>();
        _workspace = GetComponentInParent<Workspace>(); 
    }

    public override bool CanInteract()
    {
        if (_player.Interaction.HasInteraction && _player.Interaction.CurrentInteraction != (this as IInteractable)) return false;
       
        if(_workspace.IsWork) return false;

        if (_container.HasPackages == false) return false;
        if (_player.Interaction.Container.PackagesCount > 0 && 
            _player.Interaction.Container.HasPackage(PackageState.Calculated) == false) return false;


        return true;
    }

    public override void Interact()
    {
        if (!CanInteract()) return;

        var package = _container.GetPackage();

        _player.Interaction.Container.AddPackage(package);
    }
}
