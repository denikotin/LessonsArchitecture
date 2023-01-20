using UnityEngine;

namespace Assets.Scripts.Logic.CameraLogic
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _following;
        public float rotationAngleX;
        public float distance;
        public float offsetY;


        private void LateUpdate()
        {
            if (_following == null) { return; };

            Quaternion rotation = Quaternion.Euler(rotationAngleX, 0, 0);
            var position = rotation * new Vector3(0, 0, -distance) + FollowingPointPosition();
            transform.position = position;
            transform.rotation = rotation;
        }

        private Vector3 FollowingPointPosition()
        {

            Vector3 followingPosition = _following.position;
            followingPosition.y += offsetY;
            return followingPosition;
        }

        public void SetFollowing(GameObject following)
        {
            _following = following.transform;
        }
    }

}
