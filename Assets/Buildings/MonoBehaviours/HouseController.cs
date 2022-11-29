using System;
using System.Collections;
using Assets.Interfaces;
using Assets.Units.MonoBehaviours;
using UnityEngine;

namespace Assets.Buildings.MonoBehaviours
{
    public class HouseController : MonoBehaviour, ISelectableObject
    {
        public BasicUnitController PrefabUnit;
        public int PeonBuildTime;
        private bool isSelected;
        private GameObject selectedGameObject;
        private Func<Vector2> destination;
        private void Awake()
        {
            selectedGameObject = transform.Find("Selected").gameObject;
            Unselect();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private IEnumerator BuildPeon()
        {
            while (true)
            {
                yield return new WaitForSeconds(PeonBuildTime);
                var startingPosition = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
                var newPeon= Instantiate(PrefabUnit, startingPosition, Quaternion.identity);
                var peonDestination = destination();
                newPeon.NavMeshAgent.SetDestination(peonDestination);
            }
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
                    destination = target;
                    break;
                case ControlEnum.KeyDown_C:
                    StopAllCoroutines();
                    break;
                case ControlEnum.KeyDown_B:
                    StopAllCoroutines();
                    destination = target;
                    StartCoroutine(BuildPeon());
                    break;
                default:
                    Debug.LogFormat("HouseController have no action registered for ${a}", controlEnum.ToString());
                    break;
            }
        }
    }
}
