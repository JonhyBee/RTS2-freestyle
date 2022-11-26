using System.Collections;
using System.Collections.Generic;
using Assets.Interfaces;
using UnityEngine;

public class Mouse_Controller : MonoBehaviour
{

    [SerializeField] private Transform selectionAreaTransform;
    Vector3 currFramePosition;
    Vector3 lastFramePosition;
    public Vector3 newTargetPosition;
    private Vector3 startPosition;
    public List<ISelectableObject> selectedUnitList;

    // Update is called once per frame

    private void Awake()
    {
        selectedUnitList = new List<ISelectableObject>();
        selectionAreaTransform.gameObject.SetActive(false);
    }
    private void Update()
    {

        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

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

        if (Input.GetMouseButtonDown(0))
        {
            //Left mouse button pressed
            selectionAreaTransform.gameObject.SetActive(true);
            startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPosition.z = 0;

        }

        if (Input.GetMouseButtonUp(0))
        {
            //Left mouse button released, we just defined a box defined by "startPosition" and the current cursor position
            selectionAreaTransform.gameObject.SetActive(false);

            //detect all the colliders inside the selected box and created an array with them
            Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));


            foreach (var objectController in selectedUnitList)
            {
                objectController.Unselect();
            }
            //clear the selected unitlist before we update it
            selectedUnitList.Clear();

            foreach (var collider2D in collider2DArray)
            {
                //check if the selected unit is a peon
                var objectController = collider2D.GetComponent<ISelectableObject>();
                if (objectController != null)
                {
                    objectController.Select();
                    selectedUnitList.Add(objectController);
                }
                Debug.Log(selectedUnitList.Count);
            }
        }

        //Handle screen dragging
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        { //right or middle mouse button to move the camera
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;


    }



}
