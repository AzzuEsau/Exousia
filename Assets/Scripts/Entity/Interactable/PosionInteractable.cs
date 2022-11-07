using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosionInteractable : Interactable
{
    [SerializeField] private int _life = 1;
    private Player _player;

    private void Start()
    {
        _player = FindObjectOfType<Player>();
        if (_player == null)
        {
            Debug.LogWarning("No se encontro un Player en la escena");
        }
    }


    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.layer == 8)
        {
            _player.increaselife(_life);
            Destroy(gameObject);
        }
    }
}
