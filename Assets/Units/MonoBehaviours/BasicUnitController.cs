using System;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Units.MonoBehaviours
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BasicUnitController : MonoBehaviour, ISelectableObject
    {
        private GameObject selectedGameObject;
        public bool isSelected;
    public NavMeshAgent NavMeshAgent { get; private set; }

        private void OnEnable()
        {
            selectedGameObject = transform.Find("Selected").gameObject;
            NavMeshAgent = GetComponent<NavMeshAgent>();
            NavMeshAgent.updateRotation = false;
            NavMeshAgent.updateUpAxis = false;
            Unselect();
    }

        public void MoveUnit(Vector3 destination)
        {
            NavMeshAgent.SetDestination(destination);
        }

        public void Select()
        {
            selectedGameObject.SetActive(true);
        }

      public void Unselect()
      {
          isSelected = false;
          selectedGameObject.SetActive(isSelected);
      }


    public void SelectedAction(ControlEnum controlEnum, Func<Vector2> target)
        {
            switch (controlEnum)
            {
                case ControlEnum.MouseRightDown:
                    MoveUnit(target());
                    break;
                case ControlEnum.KeyDown_S:
                    MoveUnit(this.transform.position);
                    break;
                case ControlEnum.KeyDown_B:
                    TurnSprite();
                    break;
                default:
                    Debug.LogFormat("Unit_Controller have no action registered for ${a}", controlEnum.ToString());
                    break;
            }
        }

        private void TurnSprite()
        {
            GameObject[] unitWeapon = GameObject.FindGameObjectsWithTag("UnitWeapon");
            foreach (GameObject o in unitWeapon)
            {
                o.GetComponent<Animation>().Play();
            }
            this.transform.Rotate(Vector3.forward, 30);
        }
    }
}
