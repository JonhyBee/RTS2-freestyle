using System.Collections;
using System.Collections.Generic;
using Assets.Interfaces;
using UnityEngine;

public class Unit_Controller : MonoBehaviour, ISelectableObject
{
  private GameObject selectedGameObject;
  public bool is_selected;

  private void Awake()
  {
    GetComponent<MovePositionDirect>().SetMovePosition(new Vector3(10f,10f));
    selectedGameObject = transform.Find("Selected").gameObject;
    Unselect();
  }

  public void SetSelectedVisible(bool visible)
  {
    selectedGameObject.SetActive(visible);
  }
  private void Update()
  {
    if (Input.GetMouseButtonUp(1))//right click = mouve command
    {
      if (this.is_selected)
      {
        GetComponent<MovePositionDirect>().SetMovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(-0.5f,0.8f));
      }
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
