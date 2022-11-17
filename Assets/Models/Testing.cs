using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Testing : MonoBehaviour
{
  // Start is called before the first frame update

  private Grid grid;
  private void Start()
  {
    //Creation a new grid visible only in debug of size (x,y,tilesize(float),Position relative to (0,0) or lower left corner as a Vector3)     
    grid = new Grid(40, 40, 0.5f, new Vector3(0, 0));
  }

  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
      grid.SetValue(UtilsClass.GetMouseWorldPosition(), 24);

    if (Input.GetMouseButtonDown(1))
      Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
  }
}
