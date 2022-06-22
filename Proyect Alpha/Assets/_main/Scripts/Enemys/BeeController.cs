using System.Collections;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    [SerializeField] private float timingShoot;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletShootPrefab;

    private bool _isShoot;

    private readonly PoolGeneric _poolBullet = new PoolGeneric();
    
    private void Update()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.down, 20f);
        if (hit.transform.gameObject.CompareTag("Player"))
        {
            if (!_isShoot)
            {
                StartCoroutine(nameof(Shoot));
            }
            
        }
    }

    private IEnumerator Shoot()
    {
        CreateBullet();
        _isShoot = true;
        yield return new WaitForSeconds(timingShoot);
        _isShoot = false;
    }
    
    private void CreateBullet()
    {
        var bullet = _poolBullet.GetorCreate();
        
        if (bullet == null)
        {
            bullet = Instantiate(bulletShootPrefab);
            bullet.GetComponent<BulletController>().OnDesactivate += () =>
            {
                if (_poolBullet.AvailablesCount > 10)
                {
                    Destroy(bullet);
                }
                else
                {
                    _poolBullet.InUseToAvailables(bullet);
                }
            };
        }

        bullet.transform.position = shootPoint.position;
    }
}
