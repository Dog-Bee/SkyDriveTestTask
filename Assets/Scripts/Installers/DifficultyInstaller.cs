using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DifficultyInstaller", menuName = "Installers/DifficultyInstaller")]
public class DifficultyInstaller : ScriptableObjectInstaller<DifficultyInstaller>
{
    [SerializeField] private DifficultyConfig config;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DifficultyService>().AsSingle().WithArguments(config.SpawnChanceCurve, config.EmptyPlatformCurve,
            config.SpeedMultiplierCurve);
    }
}