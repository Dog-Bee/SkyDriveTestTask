using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlatformSpawnerInstaller", menuName = "Installers/PlatformSpawnerInstaller")]
public class PlatformSpawnerInstaller : ScriptableObjectInstaller<PlatformSpawnerInstaller>
{
    [SerializeField] private PlatformSpawnerConfig config;
    public override void InstallBindings()
    {
        Container.Bind<PlatformSpawnerConfig>().FromInstance(config);
    }
}