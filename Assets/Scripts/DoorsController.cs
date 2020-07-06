using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class DoorsController : MonoBehaviour
    {
        public GameObject target;
        // Start is called before the first frame update
        private void Awake()
        {
            target.GetComponent<SpriteRenderer>().enabled = false;
        }
        void Start()
        {

        }


        // Update is called once per frame
        void Update()
        {

        }

        public void Transport(GameObject player)
        {
            player.transform.position = target.transform.position;
        }
    }

}
