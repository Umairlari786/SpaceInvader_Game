using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 velocity = new Vector3(0.0f,0.1f,0.0f);
    
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(velocity);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Pool.singleton.SetScore(40);
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Pool.singleton.PlayEnemyHit();
        }
        if (collision.gameObject.tag == "Enemy1")
        {
            Pool.singleton.SetScore(10);
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Pool.singleton.PlayEnemyHit();
        }
        if (collision.gameObject.tag == "Enemy2")
        {
            Pool.singleton.SetScore(20);
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
            Pool.singleton.PlayEnemyHit();
        }
    }
}
