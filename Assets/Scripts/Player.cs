using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] [Range(0 ,1)] float limitMinX = 0f;
    [SerializeField] [Range(0, 1)] float limitMaxX = 1f;
    [SerializeField] [Range(0, 1)] float limitMinY = 0f;
    [SerializeField] [Range(0, 1)] float limitMaxY = 1f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(limitMinX, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(limitMaxX, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, limitMinY, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, limitMaxY, 0)).y;

    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
}
