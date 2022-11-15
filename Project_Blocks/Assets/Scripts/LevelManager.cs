using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;

    public Vector3[] startPositions;
    public Quaternion[] playerRotations;
    public CinemachineVirtualCamera[] cameras;
    public GameObject[] finishLines;

    private static int maxLevel = 4;

    private void Start()
    {
        currentLevel = FindObjectOfType<MainMenuManager>().targetLevel;
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int lvl)
    {
        currentLevel = lvl;
        cameras[lvl - 1].Priority = 10;
        finishLines[lvl - 1].SetActive(true);
        FindObjectOfType<PlayerController>().ResetMovement();
    }

    public void NextLevel()
    {
        if (currentLevel == maxLevel)
            SceneManager.LoadScene(0);

        cameras[currentLevel - 1].Priority = 0;
        finishLines[currentLevel - 1].SetActive(false);
        currentLevel += 1;
        cameras[currentLevel - 1].Priority = 10;
        finishLines[currentLevel - 1].SetActive(true);
    }

}
