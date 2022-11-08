using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelerInteractable : Interactable
{
    public override void Interact()
    {
        SceneManager.LoadScene("Level_0");
    }
}
