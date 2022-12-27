using System;
using Assets.Interfaces;
using UnityEngine;

namespace Assets.Controllers
{
    public class Unit_Controller : MonoBehaviour, ISelectableObject
    {
        private GameObject selectedGameObject;
        public bool isSelected;

        private void Awake()
        {
            GetComponent<MovePositionDirect>().SetMovePosition(new Vector3(10f, 10f));
            selectedGameObject = transform.Find("Selected").gameObject;
            Unselect();
        }

        public void SetSelectedVisible(bool visible)
        {
            selectedGameObject.SetActive(visible);
        }
        private void Update()
        {
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
            switch (controlEnum)
            {
                case ControlEnum.MouseRightDown:
                    GetComponent<MovePositionDirect>().SetMovePosition(target());
                    break;
                default:
                    Debug.LogFormat("Unit_Controller have no action registered for ${a}", controlEnum.ToString());
                    break;
            }
        }

        public bool IsSelected() => isSelected;
    }
}
