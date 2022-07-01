using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookX : MonoBehaviour
{

    [SerializeField] private float _speedCamera = 5;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Calculate();
    }

    void Calculate()
    {
        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        Vector3 newRotation = transform.localEulerAngles;
        newRotation.y += lookX * _speedCamera;
        newRotation.x += lookY * _speedCamera;
        transform.localEulerAngles = newRotation;
        
    }
}
