using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Vector3 velocity = new Vector3(0.0f, -0.05f, 0.0f);
    
    // Update is called once per frame
    void Start()
    {
        transform.position = new Vector2(0.0f,1.0f);
    }
    private void Update()
    {
        if (Pool.singleton.GetPlaneSpawn())
        {
            this.transform.Translate(velocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            Pool.singleton.PlayShipHit();
            Pool.singleton.SetPlaneSpawn();
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
