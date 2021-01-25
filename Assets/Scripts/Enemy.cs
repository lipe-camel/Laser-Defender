using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 1000;
    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserOffset = 1f;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float timeBetweenShots = 2f;

    bool alive = true;

    private void Start()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while (alive)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            GameObject laser = Instantiate(laserPrefab, transform.position + new Vector3(-laserOffset, 0, 0), Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(-laserSpeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Projectile") //player laser
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            ManageDamage(damageDealer);
        }
    }

    private void ManageDamage(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            ManageDeath();
        }
    }

    private void ManageDeath()
    {
        alive = false;
        Destroy(gameObject);
    }
}