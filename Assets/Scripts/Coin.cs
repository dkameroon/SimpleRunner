using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float speed = 0.2f;
    private int value;


    private void FixedUpdate()
    {
        MovingHandler();
    }
    

    private void MovingHandler()
    {
        Vector3 moveDir = new Vector3(0, 0, -1);
        transform.Translate(moveDir * (speed * Time.fixedDeltaTime));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            SoundManager.Instance.PlayPickUpSound(Camera.main.transform.position,1f);
            value++;
            GameManager.Instance.IncreaseCoins(value);
            PlayerPrefs.SetInt(PlayerPrefsNames.COLLECTED_COINS, GameManager.Instance.GetTotalCoins());
            PlayerPrefs.Save();
        }
    }

}
