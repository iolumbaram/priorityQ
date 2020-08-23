using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public enum SlotType { Available, Occupied }
    [SerializeField] SlotType slotType;
    [SerializeField] Piority slotPriority;

    Color availableColor = Color.green;
    Color occupiedColor = Color.red;
    Vector3 position;
    MeshRenderer meshRenderer;

    [Serializable]
    public class Piority
    {
        public string name;
        public int rank;
    }

    public Piority getPriority()
    {
        return slotPriority;
    }

    public SlotType getSlotType()
    {
        return slotType;
    }

    public Vector3 getSlotPosition()
    {
        return position;
    }

    public static event EventHandler<SlotEventArgs> SlotEvent;
    public class SlotEventArgs : EventArgs
    {
        public SlotEventArgs(Slot _msg)
        {
            msg = _msg;
        }

        public Slot msg { get; }
    }

    void Awake()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        toggleSlotAvailiability(slotType);
       
        this.position = this.GetComponent<Transform>().position;
    }

    void toggleSlotAvailiability(SlotType type)
    {
        switch (type)
        {
            case SlotType.Available:
                meshRenderer.material.color = availableColor;
                break;
            case SlotType.Occupied:
                meshRenderer.material.color = occupiedColor;
                slotPriority.rank = 0;
                break;
            default:
                break;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PARKED at " + this.gameObject.name);
            slotType = SlotType.Occupied;
            toggleSlotAvailiability(slotType);
            SlotEvent?.Invoke(this, new SlotEventArgs(this.GetComponent<Slot>()));
        }
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && slotType == SlotType.Occupied)
        {
            slotType = SlotType.Available;
            toggleSlotAvailiability(slotType);
            SlotEvent?.Invoke(this, new SlotEventArgs(this.GetComponent<Slot>()));
        }
    }
}
