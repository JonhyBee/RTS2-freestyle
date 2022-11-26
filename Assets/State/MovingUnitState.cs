using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.State
{
    public class MovingUnitState : BaseUnitState
    {
        public Func<Vector2> destination;
        public override void EnterState(Unit_Controller_FSM unit, Func<Vector2> target)
        {
            this.destination = target;
        }

        public override void Update(Unit_Controller_FSM unit)
        {
            if (unit.is_selected && Input.GetKeyDown(KeyCode.S))
            {
                unit.TransitionToState(unit.IdleUnitState, () => unit.Rigidbody.position);
                return;
            }
            if (unit.is_selected && Input.GetMouseButtonUp(1))
            {
                var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var mousePos2D = new Vector2(mousePosition.x, mousePosition.y);

                var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                if (hit.rigidbody != null)
                    unit.TransitionToState(unit.MovingUnitState, () => hit.rigidbody.position);
                else
                    unit.TransitionToState(unit.MovingUnitState, () => mousePosition);
            }
            var newPosition = Vector2.MoveTowards(unit.Rigidbody.position, destination(), unit.MoveSpeed * Time.deltaTime);
            if (newPosition == unit.Rigidbody.position)
                unit.TransitionToState(unit.IdleUnitState, () => unit.Rigidbody.position);
            else
                unit.Rigidbody.position = newPosition;
        }

        public override void OnCollisionEnter(Unit_Controller_FSM unit, Collision collision)
        {
            unit.TransitionToState(unit.IdleUnitState, () => unit.Rigidbody.position);
        }
    }
}
