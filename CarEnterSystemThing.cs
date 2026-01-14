
using UnityEngine;
namespace GorillaCar
{
    public class CarEnterSystemThing : MonoBehaviour
    {
        public GorillaCar system;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == "LeftHandTriggerCollider" ||
                other.gameObject.name == "RightHandTriggerCollider")
            {
                system.inCar = true;
                system.enabled = true;
            }
        }
    }
}