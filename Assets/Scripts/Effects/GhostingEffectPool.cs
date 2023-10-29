namespace Effects.Ghosting
{
    using Managers;

    using System.Collections.Generic;

    using UnityEngine;

    public class GhostingEffectPool : MonoBehaviour
    {
        [SerializeField]
        private int _maxPoolSize;

        [SerializeField]
        private GameObject _ghostEffectPrefab;

        [SerializeField]
        private string _tagToGhost;

        private Queue<GameObject> _effectFrameQueue = new Queue<GameObject>();

        public static GhostingEffectPool Instance { get; private set; }

        private bool _populatedOnce = false;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }

            Instance = this;
            _ghostEffectPrefab.GetComponent<GhostingEffect>().SetTagToGhost(_tagToGhost);
        }

        private void Update()
        {
            if (GameManager.Instance.IsArenaReady && !_populatedOnce)
            {
                PopulatePool();
            }
        }

        private void PopulatePool()
        {
            for (int i = 0; i < _maxPoolSize; i++)
            {
                var _ = Instantiate(_ghostEffectPrefab, Vector3.positiveInfinity, Quaternion.identity, transform);
            }

            _populatedOnce = true;
        }

        public void AddToPool(GameObject instance)
        {
            instance.SetActive(false);
            _effectFrameQueue.Enqueue(instance);
        }

        public GameObject ReclaimObjectFromPool()
        {
            if (_effectFrameQueue.Count == 0)
            {
                PopulatePool();
            }

            var instance = _effectFrameQueue.Dequeue();
            instance.SetActive(true);

            return instance;
        }
    }
}
