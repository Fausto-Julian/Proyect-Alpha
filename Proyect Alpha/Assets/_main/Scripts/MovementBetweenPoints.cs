using System.Collections.Generic;
using UnityEngine;

namespace _main.Scripts
{
    public class MovementBetweenPoints : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private bool isFlipSpriteX;
        [Header("Reference"), Space]
        [SerializeField] private List<Transform> positions;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private int _index = 0;
    
        private void Update()
        {
            var position = transform.position;
            position = Vector2.MoveTowards(position, positions[_index].position, speed * Time.deltaTime);
            transform.position = position;

            var distance = Vector2.Distance(position, positions[_index].position);
        
            if (distance < 0.2f)
            {
                _index++;
                if (_index >= positions.Count)
                {
                    _index = 0;
                }
            }

            if (isFlipSpriteX)
            {
                if (spriteRenderer != null)
                {
                    if ((position.x - positions[_index].position.x) < 0)
                    {
                        spriteRenderer.flipX = true;
                    }
                    else if ((transform.position.x - positions[_index].position.x) > 0)
                    {
                        spriteRenderer.flipX = false;
                    }
                }
            }
        }
    }
}