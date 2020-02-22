using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public string targetTag;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rigid.velocity = gameObject.transform.right * bulletSpeed;
        StartCoroutine(Disabler());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Transform hit = col.transform;
        if(hit.CompareTag(targetTag))
        {
            hit.GetComponent<EnemyScript>().GetDamage();
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        rigid.velocity = gameObject.transform.InverseTransformDirection(Vector3.right) * bulletSpeed;
    }

    IEnumerator Disabler()
    {
        yield return new WaitForSeconds(10f);
        gameObject.SetActive(false);
    }
}
