using Zenject;

public class MainSceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<FiguresHolder>().FromComponentInHierarchy().AsSingle();
        Container.Bind<IScoreController>().To<ScoreController>().AsSingle();
        Container.Bind<ScoreView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<EventBus>().AsSingle();
        Container.Bind<ColorsChanger>().AsSingle();
    }
}
