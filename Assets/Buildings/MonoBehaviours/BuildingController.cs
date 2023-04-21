using UnityEngine;
using UnityEngine.UI;

public class BuildingController : MonoBehaviour
{
  public GameObject[] buildingPrefabs; // an array of prefabs for each building type
  public Sprite[] buildingIcons; // an array of sprites to use for the building buttons
  public Button[] buildingButtons; // an array of buttons to use for building selection
  public LayerMask obstacleLayers; // a layer mask to determine which layers should be considered obstacles

  private int selectedBuildingIndex; // the index of the currently selected building
  private GameObject ghostObject; // the currently active ghost object
  private bool isPlacingBuilding; // whether or not the player is currently placing a building

  void Start()
  {
    // set up the building selection buttons
    for (int i = 0; i < buildingButtons.Length; i++)
    {
      int index = i; // need to create a local variable here to avoid closure issues
      buildingButtons[i].image.sprite = buildingIcons[i];
      buildingButtons[i].onClick.AddListener(() => {
      SelectBuilding(index);
      });
    }

    // set the first building in the array as the default selection
    SelectBuilding(0);
  }

  void Update()
  {
    // check if the player has clicked the mouse button
    if (Input.GetMouseButtonDown(0))
    {
      // if the player is currently placing a building, try to place it
      if (isPlacingBuilding)
      {
        TryPlaceBuilding();
      }
      // otherwise, start placing the selected building
      else
      {
       return;
      }
    }

    // if the player is currently placing a building, update the position of the ghost object
    if (isPlacingBuilding)
    {
      UpdateGhostObjectPosition();
    }
  }

  // selects a building from the array and updates the ghost object to match its shape
  void SelectBuilding(int index)
  {
    selectedBuildingIndex = index;
    ghostObject = Instantiate(buildingPrefabs[index], Vector3.zero, Quaternion.identity);
    ghostObject.GetComponent<SpriteRenderer>().sprite = buildingPrefabs[index].GetComponent<SpriteRenderer>().sprite;
    ghostObject.SetActive(true);
    isPlacingBuilding = true;
  }

  // tries to place the selected building at the current ghost object position
  void TryPlaceBuilding()
  {
    if (!IsGhostObjectObstructed())
    {
      Instantiate(buildingPrefabs[selectedBuildingIndex], ghostObject.transform.position, Quaternion.identity);
      Destroy(ghostObject);
      isPlacingBuilding = false;
    }
  }

  // updates the position of the ghost object to match the mouse position
  void UpdateGhostObjectPosition()
  {
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    ghostObject.transform.position = new Vector3(Mathf.Round(mousePos.x), Mathf.Round(mousePos.y), 0);
  }

  // checks if the current ghost object position is obstructed by any colliders in the scene
  bool IsGhostObjectObstructed()
  {
    Collider2D[] colliders = Physics2D.OverlapBoxAll(ghostObject.transform.position, ghostObject.GetComponent<BoxCollider2D>().size, 0, obstacleLayers);
    return colliders.Length > 0;
  }
}
