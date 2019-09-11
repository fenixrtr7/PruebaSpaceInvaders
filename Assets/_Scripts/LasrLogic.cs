using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasrLogic : MonoBehaviour
{
    int damage;
    bool imEnemy;
    // Start is called before the first frame update
    void Start()
    {
        damage = GetComponentInParent<Health>().hurt;
        imEnemy = GetComponentInParent<Health>().enemy;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Health>().QuitLife(damage);
        }
        else if (other.gameObject.CompareTag("Enemy") && !imEnemy)
        {
            other.gameObject.GetComponent<Health>().QuitLife(damage);
            GameManager.sharedInstance.AddScore(other.gameObject.GetComponent<EnemyController>().points);
            Destroy(gameObject);
            GameManager.sharedInstance.CheckEnemysWithLife();
        }
    }
}
