using UnityEngine;

public class SpikeHeadController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().ActiveHitAnim();
            GameManager.Instance.RestLife();
        }
    }
}
