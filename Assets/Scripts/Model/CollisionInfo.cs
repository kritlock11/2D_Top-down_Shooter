using UnityEngine;

namespace Shooter_2D_test
{
    public struct CollisionInfo
    {
        private readonly Vector3 _dir;
        private readonly float _damage;
        private readonly ContactPoint _contact;
        private readonly Transform _objCollision;
        public Vector3 Dir => _dir;
        public float Damage => _damage;
        public ContactPoint Contact => _contact;
        public Transform ObjCollision => _objCollision;

        public CollisionInfo
            (
            float damage = default,
            Vector3 dir = default,
            Transform objCollision = default,
            float scale = default,
            ContactPoint contact = default
            )
        {
            _damage = damage;
            _dir = dir;
            _contact = contact;
            _objCollision = objCollision;
        }

    }
}
