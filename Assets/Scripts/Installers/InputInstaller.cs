using Assets.Scripts.Input;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private Joystick _joystick;

    public override void InstallBindings()
    {
        Container.Bind<IInput>().
            To<Joystick>().
            FromInstance(_joystick).
            AsSingle();
    }
}