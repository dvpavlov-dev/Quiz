using UnityEngine;
using Zenject;

namespace Quiz.Infrastructure
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private CellFactory _cellFactory;
        [SerializeField] private GameProcessController _gameProcess;
        
        public override void InstallBindings()
        {
            BindCellFactory();
            BindGameProcess();
        }
        
        private void BindGameProcess()
        {
            Container
                .Bind<IGameProcess>()
                .FromInstance(_gameProcess)
                .AsSingle();
        }
        
        private void BindCellFactory()
        {
            Container
                .Bind<ICellFactory>()
                .FromInstance(_cellFactory)
                .AsSingle();
        }
    }
}