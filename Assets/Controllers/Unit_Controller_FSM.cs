using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Interfaces;
using Assets.State;
using UnityEngine;

public class Unit_Controller_FSM : MonoBehaviour, ISelectableObject
{
    private GameObject selectedGameObject;
    public bool is_selected;

    public BaseUnitState CurrentState;
    public readonly MovingUnitState MovingUnitState = new MovingUnitState();
    public readonly IdleUnitState IdleUnitState = new IdleUnitState();
    [Range(0,50)]
    public int MoveSpeed = 10;

    private Rigidbody2D rigidbody;
    public Rigidbody2D Rigidbody
    {
        get { return rigidbody; }
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
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
        CurrentState.EnterState(this, target );
    }

    public void Select()
    {
        is_selected = true;
        selectedGameObject.SetActive(is_selected);
    }

    public void Unselect()
    {
        is_selected = false;
        selectedGameObject.SetActive(is_selected);
    }
}
