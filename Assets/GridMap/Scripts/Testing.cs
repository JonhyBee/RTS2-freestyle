using Assets.CodeMonkey.Utils;
using UnityEngine;

namespace Assets.GridMap.Scripts
{
    public class Testing : MonoBehaviour {

        [SerializeField] private HeatMapVisual heatMapVisual;
        private Grid grid;

        private void Start() {
            grid = new Grid(100, 100, 0.2f, Vector3.zero);

            heatMapVisual.SetGrid(grid);
        }

        private void Update() {
            if (Input.GetMouseButtonDown(0)) {
                Vector3 position = UtilsClass.GetMouseWorldPosition();
                grid.AddValue(position, 100, 1, 15);
            }
        }
    }
}
