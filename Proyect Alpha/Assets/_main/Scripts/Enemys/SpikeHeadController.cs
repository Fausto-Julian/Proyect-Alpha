using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Transform> positions;

    private int _index = 0;
    
    private void Update()
    {
        var position = transform.position;
        position = Vector2.MoveTowards(position, positions[_index].position, speed * Time.deltaTime);
        transform.position = position;

        var distance = Vector2.Distance(position, positions[_index].position);
        
        if (distance < 1f)
        {
            _index++;
            if (_index >= positions.Count)
            {
                _index = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerController>().ActiveHitAnim();
            GameManager.Instance.RestLife();
        }
    }
}
