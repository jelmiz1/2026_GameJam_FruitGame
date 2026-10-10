using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public FruitWeaponData Data { get; private set; }
    bool hasHit;

    public void Init(FruitWeaponData data)
    {
        Data = data;

        if (data.isExplosion)
            Invoke(nameof(TimedExplosion), data.lifetime);   // explode when time runs out
        else
            Destroy(gameObject, data.lifetime);              // normal fruit just disappears
    }

    void TimedExplosion()
    {
        if (hasHit) return;
        hasHit = true;

        Explosion();
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (hasHit || Data == null) return;
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("HP")) return;
        if (other.GetComponentInParent<Fruit>()) return;     

        hasHit = true;

        if (Data.isExplosion)
        {
            Explosion();
        }
        else
        {
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
                enemy.TakeDamage(Data.damage);
        }

        Destroy(gameObject);
    }

    void Explosion()
    {
        Vector3 pos = transform.position;

        // Visual effect
        if (Data.explosion != null)
        {
            GameObject vfx = Instantiate(Data.explosion, pos, Quaternion.identity);
            Destroy(vfx, 3f);
        }

        // Sound
        if (Data.explosionSound != null)
            AudioSource.PlayClipAtPoint(Data.explosionSound, pos);

        // Damage every enemy inside the radius, once each
        var damaged = new HashSet<Enemy>();

        foreach (Collider hit in Physics.OverlapSphere(pos, Data.explosionRadius))
        {
            Enemy enemy = hit.GetComponentInParent<Enemy>();
            if (enemy != null && damaged.Add(enemy))
                enemy.TakeDamage(Data.damage);
        }
    }

}