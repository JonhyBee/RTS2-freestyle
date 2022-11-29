using UnityEngine;

namespace Assets.Units.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Unit", menuName = "New Unit", order = 0)]
    public class UnitScriptableObject : ScriptableObject
    {
        public string Name;

        public int Health;

    }
}
