using UnityEngine;

public class ParkingService : MonoBehaviour
{
    [SerializeField] SlotManager slotMgr = null;
    Slot availableSlot;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public Slot GetSlot()
    {
        if (slotMgr.pollAvailableSlot() != null)
        {
            return slotMgr.pollAvailableSlot();
        }

        return null;
    }
}
