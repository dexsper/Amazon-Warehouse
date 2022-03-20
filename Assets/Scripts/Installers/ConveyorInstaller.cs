using UnityEngine;
using Zenject;

public class ConveyorInstaller : MonoInstaller
{
    [SerializeField] private Conveyor _conveyor;
    public override void InstallBindings()
    {
        Container.Bind<Conveyor>().
            FromInstance(_conveyor).
            AsSingle();
    }
}
