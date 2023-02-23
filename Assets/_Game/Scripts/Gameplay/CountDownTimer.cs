using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
	
///<summary>
/// CountDownTimer description
///</summary>
public class CountDownTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countDownTimerText;
    private float _currentTime = 0f;

    [SerializeField] private GameObject _panelWin, _panelLose;

    private void Start()
    {
        _currentTime = GameManager.Instance.partyDuration;
        _panelLose.SetActive(false);
        _panelWin.SetActive(false);
    }

    private void Update()
    {
        _currentTime -= Time.deltaTime;
        if(_countDownTimerText != null)
        {
            _countDownTimerText.text = $"{(int)_currentTime}";
            if (_currentTime < 0.1f)
            {
                _countDownTimerText.text = "";
                WinGame();
            }
        }
    }

    public void LoseGame()
    {
        if(_currentTime>1f)
        _panelLose.SetActive(true);
    }

    private void WinGame()
    {
        _panelWin.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
