using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{

    private player _player;

    [SerializeField] private AudioClip _audioClip;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _player.UpdateCoin();
                AudioSource.PlayClipAtPoint(_audioClip,transform.position, 1f);            
                Destroy(gameObject);
            }
        }
    }
}
