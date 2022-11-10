using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    private bool isMoving;

    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private GameObject player;

    public void StartMovement()
    {
        isMoving = true;
    }

    public void ResetMovement()
    {
        isMoving = false;
        player.transform.position = levelManager.startPositions[levelManager.currentLevel - 1];
        player.transform.rotation = levelManager.playerRotations[levelManager.currentLevel - 1];
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (isMoving)
            player.GetComponent<CharacterController>().Move(levelManager.playerRotations[levelManager.currentLevel - 1] * Vector3.right * speed);
    }

    private void LevelComplete()
    {
        levelManager.NextLevel();
        ResetMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine"))
        {
            LevelComplete();
        }
    }
}
