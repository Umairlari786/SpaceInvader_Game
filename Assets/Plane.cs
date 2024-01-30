using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private float speed = 10.0f;
    Camera gameCamera;
    private float xMin = 0.0f;
    private float xMax = 0.0f;
    private float padding = 1.0f;

    private void Start()
    {
        SetupBoundaryForPlayer();
    }

    private void SetupBoundaryForPlayer()
    {
        gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
    }
    // Update is called once per frame
    void Update()
    {
        float translation = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        var newXPos = Mathf.Clamp((transform.position.x + translation), xMin, xMax);
        transform.position = new Vector2(newXPos, transform.position.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject b = Pool.singleton.Get("Bullet");
            if (b != null)
            {
                b.transform.position = this.transform.position;
                b.SetActive(true);
            }
        }
    }
}
