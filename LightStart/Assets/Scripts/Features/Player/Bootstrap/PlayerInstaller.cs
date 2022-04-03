using Zenject;

public class PlayerInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallFactories();
        InstallRules();
    }

    public void InstallFactories()
    {
        Container.Bind<PlayerFactory>().AsSingle();
    }

    public void InstallRules()
    {
        Container.BindInterfacesAndSelfTo<PlayerRules>().AsSingle();
    }
}