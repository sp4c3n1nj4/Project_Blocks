using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
    private bool isMoving = false;

    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private LevelManager levelManager;
    [SerializeField]
    private Rigidbody player;

    private void Awake()
    {
        ResetMovement();
    }

    public void StartMovement()
    {
        isMoving = true;
    }

    public void ResetMovement()
    {
        isMoving = false;
        player.velocity = Vector3.zero;
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
        {
            Vector3 desiredVelocity = levelManager.playerRotations[levelManager.currentLevel - 1] * (Vector3.right) * speed;
            player.AddForce((desiredVelocity - player.velocity) / Time.fixedDeltaTime, ForceMode.Acceleration);
        }
            
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
