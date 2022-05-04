using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : PowerUps
{
    [SerializeField] public GameObject bubblePref;
    public override void effect()
    {
        base.effect();
        StartCoroutine(PowerUpDuration(duration));
        player.shotPref = bubblePref;
        player.fireImpulse = 4;
    }

    public override IEnumerator PowerUpDuration(float time)
    {
        float fireVel = player.fireImpulse;
        GameObject shotOriginal = player.shotPref;
        yield return new WaitForSeconds(time);
        player.fireImpulse = fireVel;
        player.shotPref = shotOriginal;
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
