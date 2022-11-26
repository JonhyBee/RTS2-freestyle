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
        }

        public override void OnCollisionEnter(Unit_Controller_FSM unit, Collision collision)
        {
        }

        public override void SelectedAction(Unit_Controller_FSM unit, ControlEnum controlEnum, Func<Vector2> target)
        {
            switch (controlEnum)
            {
                case ControlEnum.MouseRightDown:
                    unit.TransitionToState(unit.MovingUnitState, target);
                    break;
                default:
                    Debug.LogFormat("IdleUnitState have no action registered for ${a}", controlEnum.ToString());
                    break;
            }
        }
    }
}