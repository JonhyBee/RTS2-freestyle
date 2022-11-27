using System;
using System.Collections;
using Assets;
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
            var newPeon= Instantiate(prefabPeon, startingPosition, Quaternion.identity);
            var peonDestination = destination();
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
                Debug.LogFormat("HouseController have no action registered for {0}", controlEnum.ToString());
                break;
        }
    }
}
