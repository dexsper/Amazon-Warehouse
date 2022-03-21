using UnityEngine;
using Zenject;

public class WorldCanvasInstaller : MonoInstaller
{
    [SerializeField]
    private WorldCanvas _canvasPrefab;


    public override void InstallBindings()
    {
        Container.Bind<WorldCanvas>().
            FromInstance(_canvasPrefab).
            AsSingle();
    }
}