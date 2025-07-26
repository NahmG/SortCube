using System;
using System.Linq;
using UnityEngine;

public class LockStack : Stack
{

    void Awake()
    {
        GameplayManager.Ins.OnSlotUnlock += UnlockSlot;
    }

    void OnDisable()
    {
        GameplayManager.Ins.OnSlotUnlock -= UnlockSlot;
    }

    public override void CreateStack()
    {
        slots = new Slot[size];
        //gen new lock slot
        for (int i = 0; i < size; i++)
        {
            //new slot
            Vector3 pos = transform.position + new Vector3(0, i * spacing, 0);
            Slot newSlot = Instantiate(slotPref, transform);
            newSlot.Init(pos, true);

            //assign new slot -> array
            slots[i] = newSlot;
        }
    }

    public void UnlockSlot()
    {
        //unlock last slot
        Slot lastLockSlot = slots.LastOrDefault(x => x.IsLock);
        if (lastLockSlot != null)
            lastLockSlot.Unlock();

        //move cube to new unlock slot
        int currentIndex = Array.IndexOf(slots, lastLockSlot);
        while (currentIndex < size - 1)
        {
            Slot currentSlot = slots[currentIndex];
            Slot prevSlot = slots[currentIndex + 1];
            if (!prevSlot.IsEmpty)
            {
                Cube cube = prevSlot.Cube;
                currentSlot.Assign(cube);
                prevSlot.Free();
                cube.AnimMoveToPosition();
            }
            currentIndex++;
        }
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < size; i++)
        {
            Vector3 position = transform.position + new Vector3(0, i * spacing);
            Gizmos.DrawWireCube(position, Vector3.one * scale);
        }

        if (Application.isPlaying)
        {
            for (int i = 0; i < size; i++)
            {
                Slot slot = slots[i];
                if (slots[i].IsLock)
                {
                    UnityEditor.Handles.Label(slot.transform.position, "Lock");
                }
            }
        }

    }
}