using Features.Config;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Installers/ConfigsInstaller")]
public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
{
    [SerializeField] private LevelConfig levelConfig;
    [SerializeField] private PlayerConfig playerConfig;
    public override void InstallBindings()
    {
        Container.Bind<LevelConfig>().AsSingle();
        Container.Bind<PlayerConfig>().AsSingle();
    }
}