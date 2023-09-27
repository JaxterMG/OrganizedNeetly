using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        //Container.Bind<ScoreController>().AsSingle().NonLazy();
        Container.Bind<IScoreController>().To<ScoreController>().AsSingle();
        Container.Bind<ScoreView>().FromComponentInHierarchy().AsSingle();
    }
}
