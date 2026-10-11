using UnityEngine;

public class FruitBush : MonoBehaviour
{
    public FruitWeaponData[] possibleFruits; // List of all possible fruits to collect from the bush
    public float fruitRespawnTime = 11.3f;

    bool hasFruit = true;

    void OnTriggerEnter(Collider other)
    {
        if(!hasFruit) // If the bush has no fruit, do nothing
        {
            return;
        }
        if(!other.TryGetComponent(out FruitInventory inventory)) //If something which doesn't have inventory script, do nothing
        {
            return;
        }
        if (gameObject.CompareTag("MagicBush"))
        {
            for (int i = 0; i < possibleFruits.Length; i++)
            {
                inventory.Add(possibleFruits[i]);
            }
        }
        if (gameObject.CompareTag("Bush"))
        {
            FruitWeaponData fruit = possibleFruits[Random.Range(0, possibleFruits.Length)];
            inventory.Add(fruit);
        }
        

        hasFruit = false; // Cooldown activates
        Invoke(nameof(Regrow), fruitRespawnTime);
    }
    void Regrow()
    {
        hasFruit = true;
    }
}
