using easyInputs;
using GorillaLocomotion;
using UnityEngine;
namespace GorillaCar
{
    public class GorillaCar : MonoBehaviour
    {



        public float acceleration = 20f;
        public float reverseAcceleration = 15f;
        public float maxSpeed = 15f;
        public float turnSpeed = 50f;
        bool thing;
        private Rigidbody carRb;


        public Rigidbody playerRb;
        public Transform seatPosition;
        public Transform exitPosition;
        public bool inCar = false;
        bool meow;

        public bool usePCInput = true; 

        void Start()
        {
            carRb = GetComponent<Rigidbody>();
            carRb.centerOfMass = new Vector3(0, -0.2f, 0);
        }

        void Update()
        {
            if (inCar)
            {
                meow = true;
            }
            if (!inCar) return;


            if (Input.GetKeyDown(KeyCode.T))
            {
                if (usePCInput)
                {
                    UnfreezePlayer();
                    playerRb.isKinematic = false;
                    meow = false;
                    this.enabled = false;

                }
            }
            if (usePCInput == false)
            {
                if (EasyInputs.GetPrimaryButtonDown(EasyHand.LeftHand))
                {
                    UnfreezePlayer();
                    playerRb.isKinematic = false;
                    meow = false;
                    this.enabled = false;
                }
            }
            if (meow)
            {
 
       
                    FreezePlayer();
                    MovePlayerToSeat();
                
            }
          
        }

        void FixedUpdate()
        {
            if (!inCar) return;

            float forwardInput = 0f;
            float turnInput = 0f;

            if (usePCInput)
            {
                forwardInput = Input.GetAxisRaw("Vertical");
                turnInput = Input.GetAxisRaw("Horizontal");
            }
            else
            {
                Vector2 leftThumb = EasyInputs.GetThumbStick2DAxis(EasyHand.LeftHand);
                forwardInput = leftThumb.y;
                turnInput = leftThumb.x;
            }

   
            if (forwardInput > 0f && carRb.velocity.magnitude < maxSpeed)
                carRb.AddForce(transform.forward * forwardInput * acceleration, ForceMode.Acceleration);
            else if (forwardInput < 0f && carRb.velocity.magnitude < maxSpeed)
                carRb.AddForce(transform.forward * forwardInput * reverseAcceleration, ForceMode.Acceleration);


            if (carRb.velocity.magnitude > 0.5f)
            {
                float turnAmount = turnInput * turnSpeed * Time.fixedDeltaTime;
                Quaternion turnOffset = Quaternion.Euler(0f, turnAmount, 0f);
                carRb.MoveRotation(carRb.rotation * turnOffset);
            }
        }

        void FreezePlayer()
        {
            playerRb.isKinematic = true;
        
   //         playerRb.detectCollisions = false;
        }

        void UnfreezePlayer()
        {
            playerRb.isKinematic = false;
            thing = false;
 //           playerRb.detectCollisions = true;
        }

        void MovePlayerToSeat()
        {

                playerRb.position = seatPosition.position;
            thing = true;
            if (thing)
            {
                playerRb.transform.position = seatPosition.position;
            }

        }


    }
}
