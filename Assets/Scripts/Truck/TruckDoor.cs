using UnityEngine;
using Zenject;

public class TruckDoor : BaseInteraction
{
    private TruckBase _truck;

    [Inject]
    private TruckSystem _truckSystem;

    [SerializeField]
    private TruckType _doorType;

    public Transform TruckSpawnPoint;

    public TruckType DoorType
    {
        get { return _doorType; }
    }

    public bool HasTruck
    {
        get { return _truck != null; }
    }

    public void SetTruck(TruckBase truck)
    {
        _truck = truck;
    }

    private void Start()
    {
        _truckSystem.AddDoor(this);
    }

    public new InteractionType InteractType
    {
        get
        {
            return InteractionType.Truck;
        }
    }

    public override bool CanInteract()
    {
        if (_player.Interaction.HasInteraction && _player.Interaction.CurrentInteraction != (this as IInteractable)) return false;
        if (HasTruck == false || _truck.ReachDoor == false) return false;

        if (DoorType == TruckType.Importation)
        {
            if (_player.Interaction.HasPackage(PackageState.New) == false) return false;
        }
        else if(DoorType == TruckType.Exportation)
        {
            if(_player.Interaction.HasPackages == false) return false;
            if (_player.Interaction.HasPackage(PackageState.Sorted) == false) return false;
        }

        return true;
    }

    public override void Interact()
    {
        if (!CanInteract()) return;
        if (_player.Interaction.Equipped) return;

        if (DoorType == TruckType.Importation)
        {
            var package = _truck.GetPackage();

            _player.Interaction.AddPackage(package);
        }
        else
        {
            var package = _player.Interaction.GetPackage();

            _truck.AddPackage(package);
        }
    }
}
