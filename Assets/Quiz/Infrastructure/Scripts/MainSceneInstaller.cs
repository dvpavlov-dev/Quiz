using Quiz.UI;
using UnityEngine;
using Zenject;

namespace Quiz.Infrastructure
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private CellFactory _cellFactory;
        [SerializeField] private GameProcessController _gameProcess;
        [SerializeField] private UserInterfaceController _userInterfaceController;
        
        public override void InstallBindings()
        {
            BindCellFactory();
            BindGameProcess();
            BindUserInterfaceController();
        }
        
        private void BindUserInterfaceController()
        {
            Container
                .Bind<IUserInterfaceController>()
                .FromInstance(_userInterfaceController)
                .AsSingle();
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