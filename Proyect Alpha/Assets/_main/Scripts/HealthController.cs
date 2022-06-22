using UnityEngine;

namespace _main.Scripts
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private float maxLife;
        [SerializeField] private Animator animator;
        private float _currentLife;

        private void Start()
        {
            _currentLife = maxLife;
        }

        public void GetDamage(float damage)
        {
            _currentLife -= damage;
            animator.SetBool("Hit", true);
            Invoke(nameof(DisableAnim), 0.5f);
            if (_currentLife < 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void DisableAnim()
        {
            animator.SetBool("Hit", false); 
        }
    }
}