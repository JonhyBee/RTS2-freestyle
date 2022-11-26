using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.State
{
    public abstract class BaseUnitState
    {
        public abstract void EnterState(Unit_Controller_FSM unit, Func<Vector2> target);

        public abstract void Update(Unit_Controller_FSM unit);

        public abstract void OnCollisionEnter(Unit_Controller_FSM unit, Collision collision);

        public abstract void SelectedAction(Unit_Controller_FSM unit, ControlEnum controlEnum, Func<Vector2> target);
    }
}
