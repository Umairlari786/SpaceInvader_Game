using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Bomb")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
