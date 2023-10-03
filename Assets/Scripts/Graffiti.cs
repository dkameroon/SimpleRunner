using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Graffiti : MonoBehaviour
{
    public static Graffiti Instance { get; private set; }
    private float speed = 3f;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        Vector3 moveDir = new Vector3(-1, 0, 0);
        transform.Translate(moveDir * speed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}