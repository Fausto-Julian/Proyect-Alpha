using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlatformFake : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Invoke("ActiveGameObject", 1f);
        }
    }

    private void ActiveGameObject()
    {
        gameObject.SetActive(true);
    }
}
