using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
  public float minSize = 5.0f;
  public float maxSize = 15.0f;
  public float sizeIncrement = 0.5f;

  void Update()
  {
    float scroll = Input.GetAxis("Mouse ScrollWheel");

    if (scroll != 0.0f)
    {
      Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - scroll * sizeIncrement, minSize, maxSize);
    }
  }
}
