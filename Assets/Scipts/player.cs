using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float jumbHeight = 15f;
    [SerializeField] private float _ammunition = 20;
    [SerializeField] private int _coin = 0;
    private bool _canFire = true;
    private bool _isWeaponActive = false;
    private gameManager _gameManager;

    [SerializeField] private GameObject _muzzleFlashParticleSystem;
    [SerializeField] private GameObject _hitMaketPrefabs;
    [SerializeField] private AudioSource _audioSource;
    private UiManager _uiManager;

    private GameObject _weapon;

    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        if (_controller == null)
        {
            Debug.LogError("Controller not working!!!");
        }
        
        _uiManager = GameObject.FindWithTag("Canvas").GetComponent<UiManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager not working!!!");
        }

        _weapon = GameObject.FindWithTag("Weapon");
        _muzzleFlashParticleSystem = GameObject.Find("Muzzle_Flash");
        
        _gameManager.HideCursor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _gameManager.showCursor();
        }
        Move();
        CheckAmmunition();
        checkHitEnemy();
        ShowAmmunition();
        ShowCoin();
    }

    void Move()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = jumbHeight;
            }
        }
        else
        {
            velocity.y -= gravity;
        }
        
        transform.Translate(velocity*Time.deltaTime, Space.Self);
    }

    void checkHitEnemy()
    {
        if (_isWeaponActive)
        {
            HitEnemy();
        }
        else
        {
           SetActiveWeapon(false); 
        }
    }
    void HitEnemy()
    {
        
        if (_canFire)
        {
            if (Input.GetMouseButton(0))
            {
                UpdateAmmunition();
                
                if (!_audioSource.isPlaying)
                {
                    _audioSource.Play();
                }
                _muzzleFlashParticleSystem.SetActive(true);
                
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit HitInfo;
                if (Physics.Raycast(ray,  out HitInfo, Mathf.Infinity))
                {
                    GameObject hitMarker = Instantiate(_hitMaketPrefabs, HitInfo.point, Quaternion.identity);
                    Destroy(hitMarker, 1.0f);
                }
            }
            else
            {
                _audioSource.Stop();
                _muzzleFlashParticleSystem.SetActive(false);
            } 
        }
        
    }

    public float getAmmunition()
    {
        return _ammunition;
    }
    
    private void SetAmmunition(int valAmm)
    {
        _ammunition = valAmm;
        CanFire(true);
    }
    private void UpdateAmmunition()
    {
        _ammunition -= 0.2f;
    }

    public void CheckAmmunition()
    {
        if (_ammunition < 1)
        {
            CanFire(false);
            if (Input.GetKeyDown(KeyCode.R))
            {
                SetAmmunition(20);
            }
            _muzzleFlashParticleSystem.SetActive(false);
        }
    }

    private void ShowAmmunition()
    {
        _uiManager.ShowAmmunition(getAmmunition());
    }

    public int GetCoin()
    {
        return _coin;
    }

    public void UpdateCoin()
    {
        _coin += 50;
    }

    public void Buy()
    {
        _coin -= 50;
    }

    private void ShowCoin()
    {
       _uiManager.ShowCoin(GetCoin()); 
    }

    public void CanFire(bool canFire)
    {
        _canFire = canFire;
    }

    public void SetActiveWeapon(bool status)
    {
        _weapon.SetActive(status);
        _isWeaponActive = status;
    }
}
