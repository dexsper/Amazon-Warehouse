
using Zenject;

public class PoolInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PoolManager>().
            FromNewComponentOnNewGameObject().
            WithGameObjectName("Pool Manager").
            AsSingle().
            NonLazy();
    }
}
