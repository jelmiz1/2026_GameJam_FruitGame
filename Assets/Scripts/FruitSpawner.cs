using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public FruitWeaponData data;

    [Header("Aiming")]
    public float startAngle = 20f;   // degrees above horizontal
    public float minAngle = -20f;
    public float maxAngle = 80f;
    public float aimSpeed = 90f;     // degrees per second

    float aimAngle;
    float cooldown;

    void Start()
    {
        aimAngle = startAngle;
        ApplyAim();
    }

    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    // Call every frame with -1..1 (W = up, S = down)
    public void AdjustAim(float input)
    {
        if (Mathf.Abs(input) < 0.01f) return;

        aimAngle = Mathf.Clamp(aimAngle + input * aimSpeed * Time.deltaTime, minAngle, maxAngle);
        ApplyAim();
    }

    void ApplyAim()
    {
        // Local rotation, so it follows the player's facing automatically
        transform.localRotation = Quaternion.Euler(-aimAngle, 0f, 0f);
    }

    public void Fire()
    {
        if (cooldown > 0f) return;

        Vector3 dir = transform.forward;

        GameObject fruit = Instantiate(data.projectilePrefab, transform.position,
                                       Quaternion.LookRotation(dir));

        if (fruit.TryGetComponent(out Fruit projectile))
            projectile.Init(data);


        if (fruit.TryGetComponent(out Rigidbody rb))
            rb.linearVelocity = dir * data.projectileSpeed;

        cooldown = 1f / data.fireRate;
    }
}