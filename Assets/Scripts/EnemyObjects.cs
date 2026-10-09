using UnityEngine;

[CreateAssetMenu(fileName = "EnemyObjects", menuName = "Scriptable Objects/EnemyObjects")]
public class EnemyObjects : ScriptableObject
{
    public string displayName;
    public float damage = 1f;
    public float health = 3f;
    public float moveSpeed = 15f;
    public AudioClip hurt;
    public AudioClip death;

}
