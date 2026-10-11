using UnityEngine;

public class FruitSpawner : MonoBehaviour
{

    [Header("Aiming")]
    public float startAngle = 20f;   // degrees above horizontal
    public float minAngle = -20f;
    public float maxAngle = 80f;
    public float aimSpeed = 90f;     // degrees per second
    public FruitInventory inventory;
    public float maxChargeTime = 1.5f;     // seconds to full power
    public float minChargePercent = 0.3f;  // a quick tap = 30% of projectileSpeed

    bool isCharging;
    float chargeStartTime;
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

    public void StartCharge()
    {
        FruitInventory.FruitSlot slot = inventory.GetCurrent();
        if (slot == null || slot.amount <= 0) return;

        if (slot.data.isRolling) // Check if scriptable objects have isRolling = true
        {
            isCharging = true;
            chargeStartTime = Time.time;
        }
        else
        {
            Fire(slot.data.projectileSpeed);   // other fruits throw immediately
        }
    }

    // Fire button released
    public void ReleaseCharge()
    {
        if (!isCharging) return;
        isCharging = false;

        FruitInventory.FruitSlot slot = inventory.GetCurrent();
        if (slot == null) return;

        float t = Mathf.Clamp01((Time.time - chargeStartTime) / maxChargeTime);
        float speed = slot.data.projectileSpeed * Mathf.Lerp(minChargePercent, 1f, t);

        Fire(speed);
    }

    void Fire(float speed)
    {
        if (cooldown > 0f) return;
        if (!inventory.TryThrowing()) return;   // no fruit, no throw

        FruitWeaponData data = inventory.GetCurrent().data;
        Vector3 dir = transform.forward;

        GameObject fruit = Instantiate(data.projectilePrefab, transform.position,
                                       Quaternion.LookRotation(dir));

        if (fruit.TryGetComponent(out Fruit projectile))
            projectile.Init(data);

        if (fruit.TryGetComponent(out Rigidbody rb))
            rb.linearVelocity = dir * speed;

        cooldown = 1f / data.fireRate;
    }
}