using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] Rigidbody2D rigidbody2D;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController.PlayerController(movementSpeed, rigidbody2D);
    }

    // Update is called once per frame
    void Update()
    {
        playerController.Update(true);
    }
}
