using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
	
///<summary>
/// Menu description
///</summary>
public class Menu : MonoBehaviour
{
    [SerializeField] private TMP_InputField _spawnText;
    [SerializeField] private TMP_InputField _durationText;

    [SerializeField] private GameObject panelBase, panelConfig;

    public void ChangeSpawn()
    {
        GameManager.Instance.enemySpawnTime = float.Parse(_spawnText.text);
    }

    public void ChangeDuration()
    {
        GameManager.Instance.partyDuration = float.Parse(_durationText.text);
    }

    public void GoBack()
    {
        panelConfig.SetActive(false);
        panelBase.SetActive(true);
    }

    public void Config()
    {
        panelConfig.SetActive(true);
        panelBase.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
