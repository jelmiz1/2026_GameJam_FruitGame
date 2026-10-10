using UnityEngine;

public class Fruit : MonoBehaviour
{
    public FruitWeaponData Data { get; private set; }

    public void Init(FruitWeaponData data)
    {
        Data = data;
        Destroy(gameObject, data.lifetime);
    }
}
