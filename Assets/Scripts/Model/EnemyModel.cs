using UnityEngine;

namespace Shooter_2D_test
{
    public class EnemyModel : BaseObjectScene
    {
        [SerializeField] private BotState _botState;

        [SerializeField] private float _hp;
        private float _maxHp = 3f;
        private float _speed = 0.5f;
        private float _ambushTimer = 0f;
        private float _startAmbushTimer = 6f;

        private float _distanseToPlayer;
        private bool _isDead = false;
        private bool _kicked = false;
        public BotVision BotVision;
        private Vector2 _point;

        public float Hp { get => _hp; set => _hp = value; }
        public float MaxHp { get => _maxHp; set => _maxHp = value; }
        public float Speed { get => _speed; set => _speed = value; }
        public bool IsDead { get => _isDead; set => _isDead = value; }
        public Vector2 Point { get => _point; set => _point = value; }
        public bool Kicked { get => _kicked; set => _kicked = value; }
        public float AmbushTimer { get => _ambushTimer; set => _ambushTimer = value; }
        public float DistanseToPlayer { get => _distanseToPlayer; set => _distanseToPlayer = value; }
        public float StartAmbushTimer { get => _startAmbushTimer; set => _startAmbushTimer = value; }
        public BotState BotState
        {
            get => _botState;
            set
            {
                _botState = value;
                switch (value)
                {
                    case BotState.Non:
                        Color = Color.white;
                        break;
                    case BotState.Patrol:
                        Color = Color.green;
                        break;
                    case BotState.Inspection:
                        Color = Color.yellow;
                        break;
                    case BotState.Detected:
                        Color = Color.red;
                        break;
                    case BotState.Ambushed:
                        Color = Color.black;
                        break;
                    case BotState.Died:
                        Color = Color.gray;
                        break;
                    default:
                        Color = Color.white;
                        break;
                }
            }
        }

        protected override void Awake()
        {
            base.Awake();
            _hp = _maxHp;
        }

        public void BotStateSwitch(BotState state, float _waitTime)
        {
            switch (state)
            {
                case BotState.Non:
                    Invoke(nameof(StateNon), _waitTime);
                    break;

                case BotState.Patrol:
                    Invoke(nameof(StatePatrol), _waitTime);
                    break;

                default:
                    Invoke(nameof(StateNon), _waitTime);
                    break;
            }
        }

        public void StateNon() => BotState = BotState.Non;
        public void StatePatrol() => BotState = BotState.Patrol;

    }
}
