using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] [Range(0, 1)] float limitMinX = 0f;
    [SerializeField] [Range(0, 1)] float limitMaxX = 1f;
    [SerializeField] [Range(0, 1)] float limitMinY = 0f;
    [SerializeField] [Range(0, 1)] float limitMaxY = 1f;
    [Header("Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float laserOffset = 1f;
    [SerializeField] float laserSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.5f;
    [Header("Health")]
    [SerializeField] int health = 1000;

    Coroutine firingCoroutine;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab,
                transform.position + new Vector3(laserOffset, 0, 0),
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(laserSpeed, 0);
            yield return new WaitForSeconds(projectileFiringPeriod);

        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(limitMinX, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(limitMaxX, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, limitMinY, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, limitMaxY, 0)).y;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy Projectile")
        {
            DamageDealer damageDealer = collision.gameObject.GetComponent<DamageDealer>();
            health -= damageDealer.GetDamage();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
