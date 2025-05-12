using Core.Player;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerModelInstaller", menuName = "Installers/PlayerModelInstaller")]
public class PlayerModelInstaller : ScriptableObjectInstaller<PlayerModelInstaller>
{
    [SerializeField] private PlayerConfig playerConfig;
    public override void InstallBindings()
    {
        Container.Bind<PlayerModel>().AsSingle().WithArguments(playerConfig);
    }
}