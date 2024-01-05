using UnityEngine;

namespace UnityMovementAI
{

    public class Wander2Unit : MonoBehaviour
    {
        public EnemyState state;
        public Transform playerPos;
        SteeringBasics steeringBasics;
        Wander2 wander;
        Flee flee;
        void Start()
        {
            steeringBasics = GetComponent<SteeringBasics>();
            wander = GetComponent<Wander2>();
            flee = GetComponent<Flee>();
            state = EnemyState.Wander;
        }

        void FixedUpdate()
        {
            Vector3 accel = new Vector3();
            float distance = Vector3.Distance(playerPos.position, transform.position);
            if(distance < 25)
            {
                accel = steeringBasics.Arrive(playerPos.position);
            }
            else
            {
                accel = wander.GetSteering();
            }
            steeringBasics.Steer(accel);
            steeringBasics.LookWhereYoureGoing();
        }
    }
}