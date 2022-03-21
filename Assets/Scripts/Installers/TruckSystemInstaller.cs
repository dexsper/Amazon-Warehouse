using UnityEngine;
using Zenject;

public class TruckSystemInstaller : MonoInstaller
{
    [SerializeField]
    private TruckSystem _truckSystem;

    public override void InstallBindings()
    {
        Container.Bind<TruckSystem>().
            FromInstance(_truckSystem).
            AsSingle().
            NonLazy();
    }
}