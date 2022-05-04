using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private GameObject shotPref;
    [SerializeField] public float fireImpulse;
    [SerializeField] private Transform fireTransform;
    public float fireRate;
    private bool canShot;

    private void Start()
    {
        canShot = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (canShot)
        {
            StartCoroutine(FireCD(fireRate));
            GameObject temp = Instantiate(shotPref, fireTransform.position, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * fireImpulse, ForceMode2D.Impulse);

            Destroy(temp, 15);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FireCD(float fireRate)
    {
        canShot = false;
        yield return new WaitForSeconds(2.0f / fireRate);
        canShot = true;
    }
}
