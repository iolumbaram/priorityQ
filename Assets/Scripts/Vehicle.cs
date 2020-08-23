using UnityEngine;
using UnityEngine.AI;

public class Vehicle : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    [SerializeField] ParkingService parkingSvc = null;

    private void Start()
    {
    }

    private void move()
    {
        agent.SetDestination(parkingSvc.GetSlot().getSlotPosition());
    }

    private void park()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //left mouse down
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }

        if (Input.GetKeyDown("space"))
        {
            move();
        }
    }
}
