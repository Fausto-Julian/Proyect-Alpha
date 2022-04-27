using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    [SerializeField] private float timingShoot;
    [SerializeField] private Transform shootPoint;
        [SerializeField] private GameObject bulletShootPrefab;
    [SerializeField] private List<Transform> positions;

    private int _index = 0;
    private bool _isShoot;

    private PoolGeneric _poolBullet = new PoolGeneric();
    
    private void Update()
    {
        var position = transform.position;
        position = Vector2.MoveTowards(position, positions[_index].position, 2 * Time.deltaTime);
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
