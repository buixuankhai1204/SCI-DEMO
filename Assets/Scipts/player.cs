using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    private CharacterController _controller;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float gravity = 1.0f;
    [SerializeField] private float jumbHeight = 15f;
    
    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Calculate();
    }

    void Calculate()
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
        
        velocity.y -= gravity;
        
        _controller.Move(velocity * Time.deltaTime);

    }
}
