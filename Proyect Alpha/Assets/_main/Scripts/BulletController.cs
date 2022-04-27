using System;
using System.Collections;
using UnityEngine;

public class BulletController : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private float timeLife;

   public Action OnDesactivate;

   private Rigidbody2D _body;

   private void OnEnable()
   {
      StartCoroutine(Desactivate());
   }

   private void Awake()
   {
      _body = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      _body.AddForce(Vector2.down * speed);
   }

   private IEnumerator Desactivate()
   {
      yield return new WaitForSeconds(timeLife);
      OnDesactivate?.Invoke();
   }
   
   private void OnTriggerEnter2D(Collider2D col)
   {
      if (col.transform.gameObject.CompareTag("Player"))
      {
         col.gameObject.GetComponent<PlayerController>().ActiveHitAnim();
         GameManager.Instance.RestLife();
         OnDesactivate?.Invoke();
      }
      else
      {
         OnDesactivate?.Invoke();
      }
   }
}