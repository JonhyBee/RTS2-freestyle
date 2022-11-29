using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Assets
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(NavMeshModifier))]
    public class TerrainTrigger : MonoBehaviour
    {
        private NavMeshModifier navMeshModifier;
        void Start()
        {
            navMeshModifier = GetComponent<NavMeshModifier>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter2D(Collider2D other)
        {
            var navMeshAgent = other.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
            {
                navMeshAgent.speed = navMeshAgent.speed / navMeshAgent.GetAreaCost(navMeshModifier.area);
            }
        }
        void OnTriggerExit2D(Collider2D other)
        {
            var navMeshAgent = other.GetComponent<NavMeshAgent>();
            if (navMeshAgent != null)
                navMeshAgent.speed = navMeshAgent.speed * navMeshAgent.GetAreaCost(navMeshModifier.area);
        }
    }
}

