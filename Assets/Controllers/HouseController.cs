using System.Collections;
using Assets.Interfaces;
using Assets.State;
using JetBrains.Annotations;
using UnityEngine;

public class HouseController : MonoBehaviour, ISelectableObject
{
    public Unit_Controller_FSM prefabPeon;
    public int PeonBuildTime;
    private bool is_selected;
    private GameObject selectedGameObject;
    private Vector2 destination;
    private void Awake()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        
        Unselect();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_selected && Input.GetMouseButtonUp(1))
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            this.destination = mousePosition;
        }
        if (is_selected && Input.GetKeyDown(KeyCode.B))
        {
            StopAllCoroutines();
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            destination = mousePosition;
            StartCoroutine(BuildPeon());
        }
        if (is_selected && Input.GetKeyDown(KeyCode.C))
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator BuildPeon()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(PeonBuildTime);
            var startingPosition = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            var newPeon= Instantiate(prefabPeon, startingPosition, Quaternion.identity);
            var peonDestination = destination;
            newPeon.TransitionToState(newPeon.MovingUnitState, () => peonDestination);
        }
    }

    public void Select()
    {
        is_selected = true;
        selectedGameObject.SetActive(is_selected);
    }

    public void Unselect()
    {
        is_selected = false;
        selectedGameObject.SetActive(is_selected);
    }
}
