using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public static Player Instance { get;private set; }
    private Rigidbody rb;
    [SerializeField] private int jumpForce;
    [SerializeField] private int currentLevelJumpForce;
    [SerializeField] private PlayerUpgradeData playerUpgradeData;
    private bool canJump;

    private void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentLevelJumpForce = PlayerPrefs.GetInt(PlayerPrefsNames.CURRENT_LEVEL_JUMP_FORCE);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           Jump();
        }
        
    }

    private void Jump()
    {
        if (!canJump)
            return;
        
        rb.AddForce((Vector3.up * (jumpForce * playerUpgradeData.JumpForceByLevel[currentLevelJumpForce].Value))/Time.fixedDeltaTime, ForceMode.Acceleration);
        SoundManager.Instance.PlayJumpSound(Camera.main.transform.position,1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canJump = false;
        }
    }
    
}
