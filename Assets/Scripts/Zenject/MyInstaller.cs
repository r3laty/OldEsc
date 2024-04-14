using Zenject;

public class MyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GetPlayerSettings>().AsSingle().NonLazy();
    }
}

public class GetPlayerSettings
{
    public float ShootingForce = 5;
    public float DashingForce = 2;
    public float MoveSpeed = 3.5f;
}