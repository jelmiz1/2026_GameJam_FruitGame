using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyObjects data;
    [SerializeField] private float currentMovementZone = 0f;

    [SerializeField] private float movementDistance;
    private float health;
    private float moveSpeed;
    private bool moveForward;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = data.health;
        moveSpeed = data.moveSpeed;
        movementDistance = data.movementDistance;
        

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if(currentMovementZone <= movementDistance && moveForward)
        {
            currentMovementZone += 1 * Time.deltaTime;
            gameObject.transform.Translate(new Vector3(1f, 0f, 0f) * moveSpeed * Time.deltaTime);
            if(currentMovementZone > movementDistance)
            {
                moveForward = false;
                UpdateFacing();
            }
        }
        else if(currentMovementZone >= 0 && !moveForward )
        {
            currentMovementZone -= 1 * Time.deltaTime;
            gameObject.transform.Translate(new Vector3(1f,0f,0f) * moveSpeed *Time.deltaTime);
            
            if(currentMovementZone < 0)
            {
                moveForward = true;
                UpdateFacing();
            }
        }
        
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        gameObject.GetComponent<AudioSource>().PlayOneShot(data.hurt);
        if(health <= 0f)
        {
            Death();
        }
    }
    public void Death()
    {
        Destroy(this.gameObject);
    }
    private void UpdateFacing()
    {
        transform.rotation = Quaternion.Euler(0f, moveForward ? 0f : 180f, 0f);
    }

}
