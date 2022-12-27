using System;
using Assets.Interfaces;
using Assets.State;
using UnityEngine;

namespace Assets.Controllers
{
    public class Unit_Controller_FSM : MonoBehaviour, ISelectableObject
    {
        private GameObject selectedGameObject;
        public bool isSelected;

        public BaseUnitState CurrentState;
        public readonly MovingUnitState MovingUnitState = new MovingUnitState();
        public readonly IdleUnitState IdleUnitState = new IdleUnitState();
        [Range(0, 50)]
        public int MoveSpeed = 10;

        private Rigidbody2D unityRigidbody2D;
        public Rigidbody2D Rigidbody2D
        {
            get { return unityRigidbody2D; }
        }

        private void Awake()
        {
            unityRigidbody2D = GetComponent<Rigidbody2D>();
            CurrentState = IdleUnitState;
            selectedGameObject = transform.Find("Selected").gameObject;
            Unselect();
        }
        private void Update()
        {
            CurrentState.Update(this);
        }

        public void OnCollisionEnter(Collision collision)
        {
            CurrentState.OnCollisionEnter(this, collision);
        }
        public void TransitionToState(BaseUnitState state, Func<Vector2> target)
        {
            CurrentState = state;
            CurrentState.EnterState(this, target);
        }

        public void Select()
        {
            isSelected = true;
            selectedGameObject.SetActive(isSelected);
        }

        public void Unselect()
        {
            isSelected = false;
            selectedGameObject.SetActive(isSelected);
        }

        public void SelectedAction(ControlEnum controlEnum, Func<Vector2> target)
        {   
            CurrentState.SelectedAction(this, controlEnum, target);
        }
        public bool IsSelected() => isSelected;
    }
}
