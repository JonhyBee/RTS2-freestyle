using System.Collections.Generic;
using Assets.Interfaces;
using Assets.Tools;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace Assets.Controllers
{
    public class Mouse_Controller : MonoBehaviour
    {

        [SerializeField] private Transform selectionAreaTransform;
        Vector2 lastFramePosition;
        public Vector3 newTargetPosition;
        private Vector3 startPosition;
        public HashSet<ISelectableObject> selectedUnitList;

        // Update is called once per frame

        private void Awake()
        {
            selectedUnitList = new HashSet<ISelectableObject>();
            selectionAreaTransform.gameObject.SetActive(false);
        }
        private void Update()
        {
            Vector2 currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        
            if (Input.GetMouseButtonDown(0))
            {
                //Left mouse button pressed
                startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPosition.z = 0;
                selectionAreaTransform.gameObject.SetActive(true);

                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
                {
                    foreach (var objectController in selectedUnitList)
                    {
                        objectController.Unselect();
                    }
                    //clear the selected unitlist before we update it
                    selectedUnitList.Clear();
                }
            }

            if (Input.GetMouseButton(0))//if the left mouse button is held down, we are defining a box, lets draw it! using the selectionAreaTransform serialized field (gameObject)
            {
                Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 lowerleft = new Vector3(
                    Mathf.Min(startPosition.x, currentMousePosition.x),
                    Mathf.Min(startPosition.y, currentMousePosition.y));

                Vector3 upperRight = new Vector3(
                    Mathf.Max(startPosition.x, currentMousePosition.x),
                    Mathf.Max(startPosition.y, currentMousePosition.y));

                selectionAreaTransform.position = lowerleft;
                selectionAreaTransform.localScale = upperRight - lowerleft;

            }

            if (Input.GetMouseButtonUp(0))
            {
                //Left mouse button released, we just defined a box defined by "startPosition" and the current cursor position
                selectionAreaTransform.gameObject.SetActive(false);

                //detect all the colliders inside the selected box and created an array with them
                Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                if(Input.GetKey(KeyCode.LeftControl))
                {
                    foreach (var collider2D in collider2DArray)
                    {
                        //check if the selected unit is a peon
                        var objectController = collider2D.GetComponent<ISelectableObject>();
                        if (objectController != null)
                        {
                            if (objectController.IsSelected())
                            {
                                objectController.Unselect();
                                selectedUnitList.Remove(objectController);
                            }
                            else
                            {
                                objectController.Select();
                                selectedUnitList.Add(objectController);
                            }                            
                        }
                    }
                }
                else
                {
                    foreach (var collider2D in collider2DArray)
                    {
                        //check if the selected unit is a peon
                        var objectController = collider2D.GetComponent<ISelectableObject>();
                        if (objectController != null)
                        {
                            objectController.Select();
                            selectedUnitList.Add(objectController);
                        }
                    }
                }
                Debug.Log(selectedUnitList.Count);
            }

            //Handle screen dragging
            if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
            { //right or middle mouse button to move the camera
                var diff = lastFramePosition - currFramePosition;
                Camera.main.transform.Translate(diff);
            }

            if (Input.GetMouseButtonUp(1))
            {
                var hit = Physics2D.Raycast(currFramePosition, Vector2.zero);
                if (hit.rigidbody != null)
                    selectedUnitList.ForEach(x => x.SelectedAction(ControlEnum.MouseRightDown, () => hit.rigidbody.position));
                else
                    selectedUnitList.ForEach(x => x.SelectedAction(ControlEnum.MouseRightDown, () => currFramePosition));
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                selectedUnitList.ForEach(x => x.SelectedAction(ControlEnum.KeyDown_S, () => currFramePosition));
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                selectedUnitList.ForEach(x => x.SelectedAction(ControlEnum.KeyDown_B, () => currFramePosition));
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                selectedUnitList.ForEach(x => x.SelectedAction(ControlEnum.KeyDown_C, () => currFramePosition));
            }

            lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
