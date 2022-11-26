using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.State
{
    public class IdleUnitState : BaseUnitState
    {
        public override void EnterState(Unit_Controller_FSM unit, Func<Vector2> target)
        {

        }

        public override void Update(Unit_Controller_FSM unit)
        {
            if (Input.GetMouseButtonUp(1))
            {
                if (unit.is_selected)
                {
                    var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    var mousePos2D = new Vector2(mousePosition.x, mousePosition.y);

                    var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
                    if (hit.rigidbody != null)
                        unit.TransitionToState(unit.MovingUnitState, () => hit.rigidbody.position);
                    else
                        unit.TransitionToState(unit.MovingUnitState, () => mousePosition);
                }
            }
        }

        public override void OnCollisionEnter(Unit_Controller_FSM unit, Collision collision)
        {
        }
    }
}