using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] int size;
    [SerializeField] float spacing;
    [SerializeField] SlotData[] slotData;
    // [SerializeField] Vector3[] slotPosition;
    Slot[] slots;

    void Awake()
    {

    }

    void Start()
    {
        Init();
    }

    public void Init()
    {
        slots = new Slot[size];

        for (int i = 0; i < size; i++)
        {
            Vector3 position = new Vector3(0, i * spacing);
            slots[i].SetPosition(position, true);
        }
    }


#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < size; i++)
        {
            Vector3 position = transform.position + new Vector3(0, i * spacing);
            Gizmos.DrawWireCube(position, Vector3.one);
        }
    }
#endif
}


