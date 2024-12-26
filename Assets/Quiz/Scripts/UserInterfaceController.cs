using System;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour, IUserInterfaceController
{
    public Action SelectedRestart { get; set; }

    [SerializeField] private TitleView _titleView;
    [SerializeField] private RestartView _restartView;
    [SerializeField] private LoadingScreenView _loadingScreen;

    public void SetTitle(string titleText, bool isShowAnimationNeeded)
    {
        _titleView.gameObject.SetActive(true);
        
        if (isShowAnimationNeeded)
        {
            _titleView.ShowTitleAnimation();
        }
        
        _titleView.SetTitle($"Найди {titleText}");
    }

    public void HideTitle()
    {
        _titleView.gameObject.SetActive(false);
    }

    public void ShowRestartView()
    {
        _restartView.gameObject.SetActive(true);
        _restartView.ShowRestartView();
    }

    public void HideRestartView(Action endedHide)
    {
        _restartView.HideRestartView(() =>
        {
            endedHide.Invoke();
            _restartView.gameObject.SetActive(false);
        });
    }

    public void ShowLoadingScreen(Action animationEnded)
    {
        _loadingScreen.gameObject.SetActive(true);
        _loadingScreen.ShowLoadingScreen(animationEnded);
    }

    public void HideLoadingScreen(Action animationEnded)
    {
        _loadingScreen.HideLoadingScreen(() =>
        {
            animationEnded?.Invoke();
            _loadingScreen.gameObject.SetActive(false);
        });
    }

    public void OnSelectedRestart()
    {
        SelectedRestart?.Invoke();
    }

    public void OnSelectedExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}

public interface IUserInterfaceController
{
    public Action SelectedRestart { get; set; }

    public void SetTitle(string titleText, bool isShowAnimationNeeded);
    
    public void HideTitle();
    
    public void ShowRestartView();

    public void HideRestartView(Action endedHide);

    public void ShowLoadingScreen(Action animationEnded);

    public void HideLoadingScreen(Action animationEnded);
}
