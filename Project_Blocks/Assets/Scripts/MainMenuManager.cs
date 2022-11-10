using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject LevelSelectInitial;
    public GameObject LevelSelectClicked;
    public TextMeshProUGUI targetLevelDisplay;

    public int targetLevel = 1;
    public int maxLevel = 5;

    private static MainMenuManager _instance;
    public static MainMenuManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SetTargetLevel(int lvl)
    {
        targetLevel = lvl;
        targetLevel = Mathf.Clamp(targetLevel, 1, maxLevel);
    }

    public void IncreaseTargetLevel(int i)
    {
        targetLevel += i;
        targetLevel = Mathf.Clamp(targetLevel, 1, maxLevel);
        targetLevelDisplay.text = targetLevel.ToString();
    }

    public void LoadLevel(int wrld)
    {
        SceneManager.LoadScene(wrld);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void LevelSelectToggle()
    {
        LevelSelectInitial.SetActive(!LevelSelectInitial.activeSelf);
        LevelSelectClicked.SetActive(!LevelSelectClicked.activeSelf);
    }
}
