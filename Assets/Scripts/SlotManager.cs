using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class SlotManager : MonoBehaviour
{
    List<Slot> slotList;
    
    private void Awake()
    {
        slotList = new List<Slot>();

        for(var i = 0; i < this.transform.childCount; i++)
        {
            if(this.transform.GetChild(i).GetComponent<Slot>() != null)
                slotList.Add(this.transform.GetChild(i).GetComponent<Slot>());
        }

        Slot.SlotEvent += Slot_SlotEvent;
    }

    private void Slot_SlotEvent(object sender, Slot.SlotEventArgs e)
    {
        Debug.Log(e.msg.name + ", " + e.msg.getSlotType());
    }

    public Slot pollAvailableSlot()
    {
        if (slotList.Count <= 0) return null;
        BubbleDown();
        return slotList[0];
    }

    public Slot peekAvailableSlot()
    {
        return new Slot();
    }

    public void addSlot(Slot slot)
    {
        slotList.Add(slot);
        BubbleUp();
    }

    void BubbleUp()
    {
        int index = slotList.Count - 1;

        while(GetParent(index) >= 0 && GetParentValue(index).rank < slotList[index].getPriority().rank)
        {
            int parentIdx = GetParent(index);
            Slot temp = slotList[index];
            slotList[index] = slotList[parentIdx];
            slotList[parentIdx] = temp;
            index = parentIdx;
        }
    }

    void BubbleDown()
    {
        int index = 0;
        while (HasLeftChild(index))
        {
            int maxIndex = GetLeftChild(index);
            if(HasRightChild(index) && GetRightChildValue(index).rank > slotList[maxIndex].getPriority().rank)
            {
                maxIndex = GetRightChild(index);
            }
            if (slotList[index].getPriority().rank >= slotList[maxIndex].getPriority().rank) break;

            Slot temp = slotList[index];
            slotList[index] = slotList[maxIndex];
            slotList[maxIndex] = temp;
            index = maxIndex;
        }
    }

    int GetLeftChild(int index)
    {
        return (index * 2) + 1;
    }

    Slot.Piority GetLeftChildValue(int index)
    {
        return slotList[GetLeftChild(index)].getPriority();
    }

    int GetRightChild(int index)
    {
        return (index * 2) + 2;
    }

    Slot.Piority GetRightChildValue(int index)
    {
        return slotList[GetRightChild(index)].getPriority();
    }

    int GetParent(int index)
    {
        return (index - 2) / 2;
    }

    Slot.Piority GetParentValue(int index)
    {
        return slotList[GetParent(index)].getPriority();
    }

    bool HasLeftChild(int index)
    {
        return GetLeftChild(index) < slotList.Count;
    }

    bool HasRightChild(int index)
    {
        return GetRightChild(index) < slotList.Count;
    }
}
