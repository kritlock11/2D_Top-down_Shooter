using UnityEngine;

namespace Shooter_2D_test
{
    public class PlayerModel : BaseObjectScene
    {
        private float _speed = 3;
        private float _x;
        private float _y;
        private int _direction;
        private Vector2 _mousePos;

        public float Speed { get => _speed; set => _speed = value; }
        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }
        public int Direction { get => _direction; set => _direction = value; }
        public Vector2 MousePos { get => _mousePos; set => _mousePos = value; }

        protected override void Awake()
        {
            base.Awake();
        }
    }
}
