using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platform
{
    public class PlayerControl : MonoBehaviour
    {

        float ypos = 0f;

        [Tooltip("Vertical Speed.")]
        public float verticalSpeed = 0.2f;

        [Tooltip("Speed.")]
        public float speed = 1f;

        [Tooltip("Jump Speed.")]
        public float jumpSpeed = 1f;

        [Tooltip("Max Jump Time.")]
        public float maxJumpTime = 0.1f;

        private float effectiveHorizontalMovement = 0f;
        private float effectiveVerticalMovement = 0f;

        private float jumpButtonPressTime;
        private float rayCastLenght = 0.005f;
        bool isJumping = false;
        bool inStairs = false;
        bool inDoor = false;
        
        private GameObject Door;

        #region Awakables
        private new Collider2D collider;
        [Tooltip("Do not drop any object here.")]
        public Rigidbody2D rigidBody;
        private float width;
        private float height;
        #endregion
        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
            collider = GetComponent<Collider2D>();
            width = collider.bounds.extents.x + 0.1f;
            height = collider.bounds.extents.y + 0.2f;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (collision.gameObject.name == "Stairs")
            {
                inStairs = true;
            }
            if (collision.gameObject.name == "door_shadow")
            {
                inDoor = true;
                Door = collision.gameObject;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Stairs")
            {
                inStairs = false;
            }
            if (collision.gameObject.name == "door_shadow")
            {
                inDoor = false;
            }
        }
       
        void Update()
        {
            gameObject.transform.Translate(new Vector3(0, ypos, 0));
        }

        private void FixedUpdate()
        {
            float horzMove = Input.GetAxisRaw("Horizontal");
            float vertMove = Input.GetAxisRaw("Vertical");
            Vector2 vect = rigidBody.velocity;

            effectiveVerticalMovement = vect.y;
            effectiveHorizontalMovement = horzMove * speed;

            // HandleJump();
            if (inStairs)
            {
                effectiveVerticalMovement = vertMove;
                rigidBody.gravityScale = 0;

            }
            else
            {
                rigidBody.gravityScale = 1;
            }
            
            
            if (inDoor && Input.GetKeyDown(KeyCode.E))
            {
                Door.GetComponent<DoorsController>().Transport(gameObject);
            }
            
           
            rigidBody.velocity = new Vector2(effectiveHorizontalMovement, effectiveVerticalMovement);


        }
        
        
    }
}

