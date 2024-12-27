using System;

namespace Quiz.Infrastructure
{
    public interface IGameProcess
    {
        public Action EndLevels { get; set; }
    
        public void StartLevel(int levelIndex, bool isStartGame);
        public void InitGameProcess();
    }
}