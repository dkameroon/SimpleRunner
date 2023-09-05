using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get;private set; }
    private Rigidbody rb;
    [SerializeField] private int jumpForce;
    [SerializeField] private int loadedJumpForce;
    private bool canJump;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        loadedJumpForce = PlayerPrefs.GetInt("JumpForce", 0);
    }

    private void Update()
    {
        if (canJump && Input.GetMouseButtonDown(0))
        {
            rb.AddForce((Vector3.up * (jumpForce + loadedJumpForce))/Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            Debug.Log("Player is grounded");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
            Debug.Log("Player is not grounded");
        }
    }
    
}
