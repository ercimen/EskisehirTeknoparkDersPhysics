using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Key Settings")] 
    [SerializeField] private KeyCode upKey;
    [SerializeField] private KeyCode downKey;
    [SerializeField] private KeyCode leftKey;
    [SerializeField] private KeyCode rightKey;

    [Header("Move Settings")]
    [SerializeField] private float speed = 2;
    private Transform _transform;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        _transform = transform;
    }

    private void Update()
    {
        Move();
        LookAtMouse();
    }

    private void Move()
    {
        if (Input.GetKey(upKey))
        {
            _transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(downKey))
        {
            _transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(leftKey))
        {
            _transform.position -= transform.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(rightKey))
        {
            _transform.position += transform.right * speed * Time.deltaTime;
        }
    }

    private void LookAtMouse()
    {
        var lookAtPos = Input.mousePosition;
        lookAtPos.z = transform.position.z - camera.transform.position.z;
        lookAtPos = camera.ScreenToWorldPoint(lookAtPos);
        lookAtPos = new Vector3(lookAtPos.x, 0, lookAtPos.z);
        //  transform.up = lookAtPos - transform.position;

        _transform.LookAt(lookAtPos);
    }
}
