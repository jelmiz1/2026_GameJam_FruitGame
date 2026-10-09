using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyObjects data;
    private float health;
    private float moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = data.health;
        moveSpeed = data.moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
       // Move();
    }
    private void Move()
    {
        gameObject.transform.Translate(new Vector3(0f, 0f, 1f) * moveSpeed * Time.deltaTime);
    }
   
}
