using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Controllers;
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
            var newPosition = Vector2.MoveTowards(unit.Rigidbody2D.position, destination(), unit.MoveSpeed * Time.deltaTime);
            if (newPosition == unit.Rigidbody2D.position)
                unit.TransitionToState(unit.IdleUnitState, () => unit.Rigidbody2D.position);
            else
                unit.Rigidbody2D.position = newPosition;
        }

        public override void OnCollisionEnter(Unit_Controller_FSM unit, Collision collision)
        {
            unit.TransitionToState(unit.IdleUnitState, () => unit.Rigidbody2D.position);
        }

        public override void SelectedAction(Unit_Controller_FSM unit, ControlEnum controlEnum, Func<Vector2> target)
        {
            switch (controlEnum)
            {
                case ControlEnum.MouseRightDown:
                    unit.TransitionToState(unit.MovingUnitState, target);
                    break;
                case ControlEnum.KeyDown_S:
                    unit.TransitionToState(unit.IdleUnitState, target);
                    break;
                default:
                    Debug.LogFormat("MovingUnitState have no action registered for ${a}", controlEnum.ToString());
                    break;
            }
        }
    }
}
