using UnityEngine;
using UnityEngine.InputSystem;

public class FruitSpawner : MonoBehaviour
{
    public FruitWeaponData data;
    public Transform character;   // the player, used for facing direction

    float cooldown;

    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    public void Fire()
    {
        if (cooldown > 0f) return;

        Vector3 dir = character.forward;

        GameObject fruit = Instantiate(data.projectilePrefab, transform.position,
                                       Quaternion.LookRotation(dir));

        if (fruit.TryGetComponent(out Rigidbody rb))
            rb.linearVelocity = dir * data.projectileSpeed;

        cooldown = 1f / data.fireRate;
    }
}