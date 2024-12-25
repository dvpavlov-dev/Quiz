using System;
using TMPro;
using UnityEngine;

public class UserInterfaceController : MonoBehaviour, IUserInterfaceController
{
    public Action SelectedRestart { get; set; }

    [SerializeField] private TMP_Text _titleText;
    [SerializeField] private GameObject _restartView;

    public void SetTitle(string titleText)
    {
        _titleText.text = $"Find {titleText}";
    }

    public void ShowRestartView()
    {
        _restartView.SetActive(true);
    }

    public void HideRestartView()
    {
        _restartView.SetActive(false);
    }

    public void OnSelectedRestart()
    {
        SelectedRestart?.Invoke();
    }
}

public interface IUserInterfaceController
{
    public Action SelectedRestart { get; set; }

    public void SetTitle(string titleText);
    
    public void ShowRestartView();

    public void HideRestartView();
}
