using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SharkShop : MonoBehaviour
{

    private player _player;
    private UiManager _uiManager;

    [SerializeField]
    private AudioClip _buySuccess;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<player>();
        _uiManager = GameObject.FindWithTag("Canvas").GetComponent<UiManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager not working!!!");
        }
        _uiManager.DisableStatusBuy();
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
                if (_player.GetCoin() >= 50)
                {
                    _player.Buy();
                    _player.CanFire(true);
                    _player.SetActiveWeapon(true);
                    AudioSource.PlayClipAtPoint(_buySuccess, transform.position, 1f);
                    _uiManager.DisableStatusBuy();
                    _uiManager.ShowBuySuccess();
                    
                }
                else
                {
                    _uiManager.DisableStatusBuy();
                    _uiManager.ShowSayGetCoin();
                }
            }
            else
            {
                _uiManager.DisableStatusBuy();
                StartCoroutine(_uiManager.ShowSayBuy());
            }
        }
        
    }

    
}
