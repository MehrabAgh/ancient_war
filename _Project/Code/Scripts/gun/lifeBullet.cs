using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lifeBullet : MonoBehaviour
{
    private int countHit { get; set; }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ammo")) countHit++;

        if (countHit > 2)
            Death();                

        Invoke("Death", 4);
    }

    private void Death() => Destroy(gameObject);
}
