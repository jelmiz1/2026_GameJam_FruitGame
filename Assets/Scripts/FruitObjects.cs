using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Fruit")]
public class FruitWeaponData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public GameObject projectilePrefab;
    public float damage = 10f;
    public float fireRate = 2f;
    public float projectileSpeed = 15f;
    public float lifetime = 5f;
    public AudioClip throwSound;
    public bool isExplosion;
    public GameObject explosion;
    public AudioClip explosionSound;
    public float explosionRadius = 3f;
}
