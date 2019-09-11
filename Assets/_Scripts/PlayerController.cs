using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    //Limites de pantalla
    public float xMin, xMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed, distanceY;
    public Boundary boundary;
    Rigidbody2D rbd;
    public GameObject shot;
    public Transform shotSpawn;

    
    [SerializeField] float timeToNextFire = 0.3f;
    float timeToFire;

    float moveHorizontal;

    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timeToFire += Time.deltaTime;
        // Disparo y recarga
        if (Input.GetKeyDown(KeyCode.F) && timeToFire >= timeToNextFire)
        {
            timeToFire = 0;

            Instantiate(shot, shotSpawn.position, Quaternion.identity,this.transform);
            //GetComponent<AudioSource>().Play();
        }
    }

    void FixedUpdate()
    {
        // movimiento nave
        moveHorizontal = Input.GetAxis("Horizontal");
    
        Vector2 movement = new Vector2(moveHorizontal, distanceY);
        rbd.velocity = new Vector2 (movement.x * speed, distanceY);

        // Limites donde se puede mover
        rbd.position = new Vector2(Mathf.Clamp(rbd.position.x, boundary.xMin, boundary.xMax), distanceY);
    }
}