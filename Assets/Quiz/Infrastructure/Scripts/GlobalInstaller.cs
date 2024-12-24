using UnityEngine;
using Zenject;

namespace Quiz.Infrastructure
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private LevelsConfigSource _config; 
    
        public override void InstallBindings()
        {
            BindConfig();
        }
        
        private void BindConfig()
        {
            Container
                .Bind<LevelsConfigSource>()
                .FromInstance(_config)
                .AsSingle();
        }
    }
}