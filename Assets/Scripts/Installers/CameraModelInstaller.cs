using Core.Camera;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "CameraModelInstaller", menuName = "Installers/CameraModelInstaller")]
public class CameraModelInstaller : ScriptableObjectInstaller<CameraModelInstaller>
{
    [SerializeField] private CameraConfig cameraConfig;
    public override void InstallBindings()
    {
        Container.Bind<CameraModel>().AsSingle().WithArguments(cameraConfig);
    }
}