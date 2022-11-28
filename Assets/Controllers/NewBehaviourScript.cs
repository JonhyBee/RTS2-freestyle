using UnityEngine;

namespace Assets.Controllers
{
    public class NewBehaviourScript : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            print("Another object has entered the trigger");
        }
        // Use this for initialization  
        void Start() { }
        // Update is called once per frame  
        void Update() { }
    }
}