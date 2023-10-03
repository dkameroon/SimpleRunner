using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public static Obstacle Instance { get; private set; }
    private float speed = 5f;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(1, 0, 0);
        transform.Translate(moveDir * speed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.GameOver = true;
            SoundManager.Instance.PlayDefeatSound(Camera.main.transform.position,1f);
        }
    }



}
