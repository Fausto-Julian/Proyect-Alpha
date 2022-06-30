using System.Collections;
using UnityEngine;

public class BeeController : MonoBehaviour
{
    [SerializeField] private float timingShoot;
    [SerializeField] private float shootDistance;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletShootPrefab;
    [SerializeField] private GameObject player;

    private bool _isShoot = false;

    private readonly PoolGeneric _poolBullet = new PoolGeneric();

    

    private void Update()
    {
        Vector3 playerDirection = (player.transform.position - transform.position).normalized;

        var hit = Physics2D.Raycast(transform.position, playerDirection, shootDistance);
        
        if (hit.transform.gameObject.CompareTag("Player"))
        {
            Debug.Log("tag comparada");
            if (!_isShoot)
            {
                StartCoroutine(nameof(Shoot));
                Debug.Log("SHOT");
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

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, (player.transform.position - transform.position).normalized, Color.red);
    }
}
