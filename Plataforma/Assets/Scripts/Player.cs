using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] public float vel;
    [SerializeField] public float jumpForce;
    float jump;
    [SerializeField] public float maxJump;

    //shot
    [SerializeField] public GameObject shotPref;
    [SerializeField] public float fireImpulse;
    [SerializeField] private Transform fireTransform;
    public float fireRate;
    private bool canShot;
    private int rotationFire;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jump = 0;
        canShot = true;
    }

    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        rb.AddRelativeForce(Vector2.right * inputHorizontal * vel * Time.deltaTime);

        if (inputHorizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rotationFire = 1;
        }
        if (inputHorizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rotationFire = -1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && jump < maxJump)
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1") && canShot)
        {
            StartCoroutine(FireCD(fireRate));
            GameObject temp = Instantiate(shotPref, fireTransform.position, Quaternion.identity);
            temp.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * fireImpulse * rotationFire, ForceMode2D.Impulse);

            Destroy(temp, 5);
        }
    }

    void Jump()
    {
        rb.AddRelativeForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jump++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grounded"))
        {
            jump = 0;
        }

        if (collision.gameObject.CompareTag("ZonaMuerte") || collision.gameObject.CompareTag("Enemy"))
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public IEnumerator FireCD(float fireRate)
    {
        canShot = false;
        yield return new WaitForSeconds(1.0f / fireRate);
        canShot = true;
    }
}
