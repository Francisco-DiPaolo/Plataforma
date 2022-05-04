using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    private Rigidbody2D m_rig;
    public float speed;
    
    void Start()
    {
        m_rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        m_rig.velocity = new Vector2(speed, m_rig.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Plataforma")
        {
            speed *= -1;
            this.transform.localScale = new Vector2(this.transform.localScale.x * -1, this.transform.localScale.y);
        }
    }
}
