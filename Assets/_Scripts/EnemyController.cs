using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int points = 10;

    public float speed, distanceY;
    Rigidbody2D rbd;
    float moveHorizontal;
    public Boundary boundary;

    // Shot
    public GameObject shot;
    public Transform shotSpawn;

    // Fire
    float fireRate;
    float minFireRate = 0.2f;
    private float nextFire;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        moveHorizontal = 1;

        fireRate = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        // Disparo y recarga automatico
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, Quaternion.identity, this.transform);
            //GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveHorizontal, distanceY);
        rbd.velocity = new Vector2(movement.x * speed, distanceY);

        // Limites donde se puede mover
        rbd.position = new Vector2(Mathf.Clamp(rbd.position.x, boundary.xMin, boundary.xMax), distanceY);

        if(rbd.position.x == boundary.xMax)
        {
            moveHorizontal = -1;
            AddRate();
        }
        else if(rbd.position.x == boundary.xMin)
        {
            moveHorizontal = 1;
            AddRate();
        }
    }

    // Reducir tiempo de disparo
    public void AddRate()
    {
        // Bajamos en "Y"
        distanceY -= 0.2f;

        if (fireRate <= minFireRate)
        {
            fireRate = minFireRate;
        }
        else
        {
            fireRate -= 0.05f;
        }
    }
}
