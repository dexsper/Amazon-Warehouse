using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class TruckDoor : BaseInteraction
{
    [Header("Door Settings")]
    [SerializeField]
    private TruckType _doorType;

    [Header("Truck Settings")]
    [Range(5, 30f)]
    [SerializeField] private float _truckSpawnDistance = 15f;

    [Inject]
    private TruckSystem _truckSystem;
    private TruckBase _truck;


    public Vector3 TruckSpawnPoint { get; private set; }
    public TruckType DoorType => _doorType;
    public bool HasTruck => _truck != null;

    public UnityEvent<TruckBase> OnTruckUpdated;
    public UnityEvent OnInteract;

    protected override void Start()
    {
        base.Start();

        _truckSystem.AddDoor(this);

        TruckSpawnPoint = transform.position + -transform.right * _truckSpawnDistance;
    }
    public void SetTruck(TruckBase truck)
    {
        _truck = truck;

        OnTruckUpdated?.Invoke(_truck);
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
            if (_truck.Container.HasPackages == false) return false;

            if (_player.Interaction.Container.Equipped) return false;
            if (_player.Interaction.Container.PackagesCount > 0 && _player.Interaction.Container.HasPackage(PackageState.New) == false) return false;
        }
        else if (DoorType == TruckType.Exportation)
        {
            if (_truck.Container.Equipped) return false;

            if (_player.Interaction.Container.HasPackages == false) return false;
            if (_player.Interaction.Container.PackagesCount > 0 && _player.Interaction.Container.HasPackage(PackageState.Sorted) == false) return false;
        }

        return true;
    }
    public override void Interact()
    {
        if (!CanInteract()) return;

        if (DoorType == TruckType.Importation)
        {
            if (_player.Interaction.Container.Equipped) return;

            var package = _truck.Container.GetPackage();

            _player.Interaction.Container.AddPackage(package);
        }
        else
        {
            var package = _player.Interaction.Container.GetPackage();

            _truck.Container.AddPackage(package);
        }

        OnInteract?.Invoke();
    }

}
