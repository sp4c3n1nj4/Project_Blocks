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

    private static int maxLevel = 5;

    private void Start()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int lvl)
    {
        currentLevel = lvl;
        cameras[lvl - 1].Priority = 100;
    }

    public void NextLevel()
    {
        if (currentLevel == maxLevel)
            SceneManager.LoadScene(0);

        cameras[currentLevel - 1].Priority = 0;
        currentLevel += 1;
        cameras[currentLevel - 1].Priority = 100;
    }

}
