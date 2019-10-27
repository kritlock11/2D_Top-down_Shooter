using System.Collections.Generic;
using UnityEngine;

namespace Shooter_2D_test
{
    public sealed class Main : MonoBehaviour
    {
        [HideInInspector] public PlayerModel PlayerModel;
        [HideInInspector] public SpawnerModel BarakPrefab;
        [HideInInspector] public EnemyModel EnemyModelPrefab;
        [HideInInspector] public GameObject EffectPrefab;
        [SerializeField] private GameObject ControlPanel;

        public Camera MainCamera { get; private set; }
        public Inventory Inventory { get; private set; }
        public EnemyController EnemyController { get; private set; }
        public InputController InputController { get; private set; }
        public WeaponController WeaponController { get; private set; }
        public PlayerController PlayerController { get; private set; }
        public SpawnerController SpawnerController { get; private set; }
        public CamShakeController CamShakeController { get; private set; }
        


        public readonly List<IOnStart> Inites = new List<IOnStart>();
        public readonly List<IOnUpdate> Updates = new List<IOnUpdate>();
        public readonly List<IOnFixedUpdate> FixedUpdates = new List<IOnFixedUpdate>();

        private void Awake()
        {

            BarakPrefab = Resources.Load<SpawnerModel>("Prefabs/Barak");
            EnemyModelPrefab = Resources.Load<EnemyModel>("Prefabs/Enemy");
            EffectPrefab = Resources.Load<GameObject>("ParticlesPrefabs/BotDeathEffect");

            MainCamera = Camera.main;

            PlayerModel = FindObjectOfType<PlayerModel>();

            InputController = new InputController();
            Updates.Add(InputController);

            PlayerController = new PlayerController();
            Inites.Add(PlayerController);
            Updates.Add(PlayerController);
            FixedUpdates.Add(PlayerController);

            SpawnerController = new SpawnerController();
            Inites.Add(SpawnerController);
            Updates.Add(SpawnerController);

            EnemyController = new EnemyController();
            Inites.Add(EnemyController);
            Updates.Add(EnemyController);

            WeaponController = new WeaponController();

            Inventory = new Inventory();
            Inites.Add(Inventory);

            CamShakeController = new CamShakeController();
            Inites.Add(CamShakeController);
        }

        private void Start()
        {
            for (int i = 0; i < Inites.Count; i++)
            {
                Inites[i].OnStart();
            }
        }

        public void InvokeSometh(string action, float time)
        {
            Invoke(action, time);
        }

        public void BlackDeath()
        {
            PlayerController.BlackDeath();
        }

        public void ControlsPanelSwitch(bool value)
        {
            ControlPanel.SetActive(value);
        }
    }
}
