using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour
{
  public GameObject[] buildingPrefabs; // an array of prefabs for each building type
  public Sprite[] buildingIcons; // an array of sprites to use for the building buttons
  public Button[] buildingButtons; // an array of buttons to use for building selection
  public LayerMask obstacleLayers; // a layer mask to determine which layers should be considered obstacles

  private int selectedBuildingIndex; // the index of the currently selected building
  private bool isPlacingBuilding; // whether or not the player is currently placing a building

  private GameObject[] ghostObjects;

  void Start()
  {
    // Initialize the array of ghost objects
    ghostObjects = new GameObject[buildingPrefabs.Length];

    // set up the building selection buttons
    for (int i = 0; i < buildingButtons.Length; i++)
    {
      int index = i; // create a local variable here to avoid closure issues
      buildingButtons[i].image.sprite = buildingIcons[i];
      buildingButtons[i].onClick.AddListener(() => SelectBuilding(index));

      // Create a ghost object for each building prefab
      ghostObjects[i] = Instantiate(buildingPrefabs[i], Vector3.zero, Quaternion.identity);
      ghostObjects[i].SetActive(false);

      Debug.Log($"Created ghost object for building {i}: {ghostObjects[i]}");
    }
  }



  void Update()
  {
    // if the player is currently placing a building, update the position of the ghost object
    if (isPlacingBuilding)
    {
      UpdateGhostObjectPosition();
    }

    // check if the player has clicked the mouse button
    if (Input.GetMouseButtonDown(0))
    {
      // if the player is currently placing a building, try to place it
      if (isPlacingBuilding)
      {
        TryPlaceBuilding();
      }
    }
  }

  void SelectBuilding(int index)
  {
    Debug.Log($"Selecting building {index}: {ghostObjects[index]}");

    // if a building is currently being placed, stop placing it
    if (isPlacingBuilding)
    {
      ghostObjects[selectedBuildingIndex].SetActive(false);
    }

    selectedBuildingIndex = index;
    ghostObjects[selectedBuildingIndex].SetActive(true);
    isPlacingBuilding = true;
  }


  void TryPlaceBuilding()
  {
    if (IsGhostObjectObstructed())
    {
      ghostObjects[selectedBuildingIndex].SetActive(false);
      isPlacingBuilding = false;
    }
    else
    {
      Instantiate(buildingPrefabs[selectedBuildingIndex], ghostObjects[selectedBuildingIndex].transform.position, Quaternion.identity);
      ghostObjects[selectedBuildingIndex].SetActive(false);
      isPlacingBuilding = false;
    }
  }



  // updates the position of the ghost object to match the mouse position
  void UpdateGhostObjectPosition()
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    ghostObjects[selectedBuildingIndex].transform.position = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);
  }

  bool IsGhostObjectObstructed()
  {
    BoxCollider2D ghostCollider = ghostObjects[selectedBuildingIndex].GetComponent<BoxCollider2D>();
    Vector2 size = ghostCollider.size * ghostCollider.transform.localScale;
    Collider2D[] colliders = Physics2D.OverlapBoxAll(ghostObjects[selectedBuildingIndex].transform.position, size, 0, obstacleLayers);
    foreach (Collider2D collider in colliders)
    {
      // Ignore the ghost object's own collider
      if (collider.gameObject != ghostObjects[selectedBuildingIndex])
        return true;
    }
    return false;
  }


}
