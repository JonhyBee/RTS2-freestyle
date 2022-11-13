using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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