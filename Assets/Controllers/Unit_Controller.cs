using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Controller : MonoBehaviour
{
  private GameObject selectedGameObject;
  public bool is_selected;

  private void Awake()
  {
    selectedGameObject = transform.Find("Selected").gameObject;
    SetSelectedVisible(false);
  }

  public void SetSelectedVisible(bool visible)
  {
    selectedGameObject.SetActive(visible);
  }
  private void Update()
  {
    if (Input.GetMouseButtonUp(1))//right click = mouve command
    {
      if (GetComponent<Unit_Controller>().is_selected)
      {
        GetComponent<MovePositionDirect>().SetMovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
      }
    }
  }

}
