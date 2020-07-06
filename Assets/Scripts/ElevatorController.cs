using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    enum ElevatorState { stopped, movingUp, movingDown, moving }
    public class ElevatorController : MonoBehaviour
    {
        private Vector3 posA;

        private Vector3 posB;

        private Vector3 nextPos;

        public float speed;

        public Transform childTransform;

        public Transform transformB;

        private ElevatorState elevatorState = ElevatorState.movingUp;

        // Start is called before the first frame update
        void Start()
        {
            posA = childTransform.localPosition;
            posB = transformB.localPosition;
            nextPos = posB;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (elevatorState != ElevatorState.stopped)
            {
                Move();
            }
            
        }

        private void Move()
        {
            childTransform.localPosition = Vector3.MoveTowards(childTransform.localPosition, nextPos, speed * Time.deltaTime);
            if (elevatorState != ElevatorState.stopped && Vector3.Distance(childTransform.localPosition, nextPos) <= 0)
            {
                StartCoroutine(StateController());
            }
        }

      

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("ELEVATOR: playerON");
                collision.transform.SetParent(childTransform);
            }
        }
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("ELEVATOR: playerOFF");
                collision.transform.SetParent(null);
            }
        }
        private IEnumerator StateController()
        {
            var previousState = elevatorState;
            elevatorState = ElevatorState.stopped;
            yield return new WaitForSeconds(2);
            elevatorState = previousState;
            switch (elevatorState)
            {
                case (ElevatorState.movingDown):
                    nextPos = posB;
                    elevatorState = ElevatorState.movingUp;
                    
                    break;
                case (ElevatorState.movingUp):
                    nextPos = posA;
                    elevatorState = ElevatorState.movingDown;
                    
                    break;
            }
            
        }
    }
}

