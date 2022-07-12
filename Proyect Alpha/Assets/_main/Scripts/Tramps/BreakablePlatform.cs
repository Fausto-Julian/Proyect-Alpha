using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BreakablePlatform : MonoBehaviour
{
    private SpriteRenderer[] _renderers;

    private void Start()
    {
        _renderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(nameof(DisablePlatform));
        }
    }

    private IEnumerator DisablePlatform()
    {
        for (var i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].color = Color.grey;
        }

        yield return new WaitForSeconds(2f);
        
        gameObject.SetActive(false);
    }

}
