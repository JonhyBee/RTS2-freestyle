using Assets;
using Assets.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoggingCampController : MonoBehaviour, ISelectableObject
{
  public float radius; // Radius of the circular area around the building
  private bool isSelected;
  private GameObject selectedGameObject;
  public Color circleColor = new Color(0.0f, 1.0f, 0.0f, 0.3f); // Light green transparent circle color
  private GameObject circleObject; // Reference to the GameObject representing the circle
  private void Awake()
  {
    selectedGameObject = transform.Find("Selected").gameObject;
    Unselect();
  }

  void Start()
  {
    // Find all the game objects that have "Tree" in their names inside the circular area
    Dictionary<GameObject, Transform> treesInCircle = new Dictionary<GameObject, Transform>();
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
    foreach (Collider2D collider in colliders)
    {
      if (collider.gameObject.name.Contains("Tree"))
      {
        treesInCircle.Add(collider.gameObject, collider.gameObject.transform);
      }
    }

    // Print out the dictionary for debugging purposes
    foreach (KeyValuePair<GameObject, Transform> tree in treesInCircle)
    {
      Debug.Log(tree.Key.name + " is at location " + tree.Value.position);
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
      case ControlEnum.KeyDown_B:
        ShowCircle();
        break;
      default:
        Debug.LogFormat("HouseController have no action registered for ${a}", controlEnum.ToString());
        break;
    }
  }
  public bool IsSelected() => isSelected;

  public void ShowCircle()
  {
    // Create a new GameObject representing the circle if it doesn't already exist
    if (circleObject == null)
    {
      circleObject = new GameObject("Circle");
      circleObject.transform.parent = transform; // Make the circle a child of the building
      circleObject.transform.localPosition = Vector3.zero; // Center the circle around the building
      circleObject.AddComponent<SpriteRenderer>(); // Add a SpriteRenderer component to the circle
    }

    // Set the SpriteRenderer component's color and size to match the circle
    SpriteRenderer circleRenderer = circleObject.GetComponent<SpriteRenderer>();
    circleRenderer.color = circleColor;
    circleRenderer.sprite = Sprite.Create(
        texture: new Texture2D(1, 1),
        rect: new Rect(0, 0, 2 * radius, 2 * radius),
        pivot: new Vector2(0.5f, 0.5f)
    );
  }
}

