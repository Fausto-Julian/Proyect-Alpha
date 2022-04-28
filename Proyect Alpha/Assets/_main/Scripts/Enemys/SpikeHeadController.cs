using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().ActiveHitAnim();
            GameManager.Instance.RestLife();
        }
    }
}
