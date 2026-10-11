using UnityEngine;
using System;
using System.Collections.Generic;

public class FruitInventory : MonoBehaviour
{
    [Serializable]
    public class FruitSlot
    {
        public FruitWeaponData data;
        public int amount;
    } // A class to hold data about the Fruit weapon and the amount
    public List<FruitSlot> slots = new List<FruitSlot>(); //List for the FruitSlot class
    public int currentIndex; //To check the index of list
    public event Action OnChanged;   // UI can listen to events

    public void Add(FruitWeaponData fruit, int count = 1)
    {
        FruitSlot slot = slots.Find(s => s.data == fruit); // looks for fruit slots, if there is already the same data
        if(slot == null)
        {
            slot = new FruitSlot { data = fruit }; //if the fruit is not there, add new fruit into slots list 
            slots.Add(slot);
        }
        slot.amount += count; // Add fruit to the total amount of the fruit
        OnChanged?.Invoke();
    }
    public bool TryThrowing()
    {
        FruitSlot current = GetCurrent();
        if(current == null || current.amount <= 0) // Check if current index in slots has any fruit, if it does reduces the amount of fruit that is currently in the FruitSlot list index
        {
            return false;
        }
        current.amount--;
        OnChanged?.Invoke();
        return true;
        

    }
    public void Next()
    {
        /*if (slots.Count == 0) return; With no slots, there’s nothing to switch to.
        The for loop looks at the slots after the current one, one at a time: 1 step ahead, 2 steps ahead, and so on, up to a full round.
        (currentIndex + i) % slots.Count is the trick that makes it wrap around to the start. % (modulo) gives the remainder of a division, so the number can never go past the end of the list.
        if (slots[index].count > 0) If that slot has fruit, it selects it, tells the UI, and returns (stops looking).
        If the loop finishes without finding anything, you have no fruit at all, so the selection stays where it was. */
        if (slots.Count == 0) return;

        // Move to the next fruit you actually have
        for (int i = 1; i <= slots.Count; i++)
        {
            int index = (currentIndex + i) % slots.Count;
            if (slots[index].amount > 0)
            {
                currentIndex = index;
                OnChanged?.Invoke();
                return;
            }
        }
    }
    public FruitSlot GetCurrent()
    {
        if (slots.Count > 0)
        {
            return slots[currentIndex];
        }
           

        return null;
    }

}
