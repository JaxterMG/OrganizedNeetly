using Core.Grid.Figures;
using Core.Score;
using Core.UI.Score;
using Zenject;
using Core.EventBus;

namespace Core
{
    public class MainSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<FiguresHolder>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IScoreController>().To<ScoreController>().AsSingle();
            Container.Bind<ScoreView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<EventBus.EventBus>().AsSingle();
            Container.Bind<ColorsChanger>().AsSingle();
        }
    }
}
