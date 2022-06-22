using UnityEngine;

public class AngryPingController : MonoBehaviour
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