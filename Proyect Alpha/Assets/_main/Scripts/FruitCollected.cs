using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FruitCollected : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.gameObject.CompareTag("Player"))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            
            GameManager.Instance.AddFruit();
            
            Invoke(nameof(DesableItem), 0.5f);
        }
    }

    private void DesableItem()
    {
        gameObject.SetActive(false);
    }
}
