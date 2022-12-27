using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Interfaces
{
    public interface ISelectableObject
    {
        void Select();
        void Unselect();
        void SelectedAction(ControlEnum controlEnum ,Func<Vector2> target);
        bool IsSelected();
    }
}
