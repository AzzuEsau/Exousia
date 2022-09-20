using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerController.Update(true);
        playerController.IsRunning();
    }
}
