using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Platform
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ElevatorManual : MonoBehaviour
    {

        private Vector3 posA;

        private Vector3 posB;

        private Vector3 nextPos;

        public float speed;

        public List<Transform> elevatorFloorStops = new List<Transform>();

        private List<Vector3> positions = new List<Vector3>();


        private ElevatorState elevatorState = ElevatorState.moving;

        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform elevatorFloor in elevatorFloorStops)
            {
                positions.Add(elevatorFloor.localPosition);
                Debug.Log(positions);


            }
            //nextPos = elevatorFloorStops[0].localPosition;
            posA = positions[0];
            posB = positions[1];
            nextPos = posB;
            for (int i = 0; i < positions.Count; i++)
            {
                Debug.Log(positions[i]);
            }
        }

        void FixedUpdate()
        {
            if (elevatorState != ElevatorState.stopped)
            {
                //Que no se mueva a menos que el player de la orden, para arriba o para abajo
                Move();
            }

        }

        private void Move()
        {
            elevatorFloorStops[0].localPosition = Vector3.MoveTowards(elevatorFloorStops[0].localPosition, nextPos, speed * Time.deltaTime);
            if (Vector3.Distance(elevatorFloorStops[0].localPosition, nextPos) <= 0)
            {
                StartCoroutine(StateController());
            }
        }



        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("ELEVATOR: playerON");
                collision.transform.SetParent(elevatorFloorStops[0]);
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
            bool cambio = false;
            WaitForSeconds wait = new WaitForSeconds(2);

            /*
             * si posB es positionCount entonces nextPos es posA
             * si posB no es positionCount posA posicion+a posB posicion+1 nextPos es posB
             * si nextPos es posA y posA no es posicion 0 entonces posA posicion-1 posB posicion-1 nextPos es posA
             * si nextPos es posA y posA es posicion 0 entonces nextPos es posB
             */
            // estoy ascendiendo y llegue al ultimo piso
            if (nextPos == posB && posB == positions[positions.Count - 1])
            {
                Debug.Log("posB == positions[positions.Count - 1]");
                nextPos = posA;
                cambio = true;
            }
            // estoy descendiendo y llegue al primer piso.
            if (nextPos == posA && posA == positions[0])
            {
                Debug.Log("posA == positions[0]");
                nextPos = posB;
                cambio = true;
            }
            // descendiendo
            if (!cambio && nextPos == posA && posA != positions[0])
            {
                Debug.Log("nextPos == posA && posA != positions[0]");
                posA = positions[positions.IndexOf(posA) - 1];
                posB = positions[positions.IndexOf(posB) - 1];
                nextPos = posA;
            }
            // ascendiendo
            if (!cambio && nextPos == posB && posB != positions[positions.Count - 1])
            {
                Debug.Log("nextPos == posB && posB != positions[positions.Count - 1]");
                posA = positions[positions.IndexOf(posA) + 1];
                posB = positions[positions.IndexOf(posB) + 1];
                nextPos = posB;
            }



            elevatorState = ElevatorState.stopped;
            yield return wait;
            elevatorState = ElevatorState.moving;


        }
    }

}


