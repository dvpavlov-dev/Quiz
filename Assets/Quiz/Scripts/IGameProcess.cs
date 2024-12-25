using System;
public interface IGameProcess
{
    public Action EndLevels { get; set; }
    
    public void StartLevel();
}
