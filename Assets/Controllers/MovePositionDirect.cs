using Assets.Interfaces;
using UnityEngine;

namespace Assets.Controllers
{
    public class MovePositionDirect : MonoBehaviour
    {

        private Vector3 movePosition;

        public void Awake()
        {
            movePosition = transform.position;
        }

        public void SetMovePosition(Vector3 movePosition)
        {
            this.movePosition = movePosition;
        }

        private void Update()
        {
            Vector3 moveDir = (movePosition - transform.position).normalized;
            GetComponent<IMoveVelocity>().SetVelocity(moveDir);
        }
    }
}
