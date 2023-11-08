namespace Managers
{
    using Configuration.DecorationConfiguration;

    using PowerUps;

    using System.Collections;
    using System.Collections.Generic;

    using UnityEngine;

    public class PowerUpManager : MonoBehaviour
    {
        private PowerUpAttributes _powerUpAttributes;
        [SerializeField]
        private List<GameObject> _powerups = new List<GameObject>();

        [SerializeField]
        private List<GameObject> _availablePowerUps = new List<GameObject>();
        private float _interval;

        [SerializeField]
        private Transform[] _spawnLocations;

        private int _lastSpawnIndex = -1;

        public void OnInitialized(DecorationConfiguration decorationConfiguration)
        {
            _powerUpAttributes = decorationConfiguration.powerUpAttributes;
            _interval = _powerUpAttributes.spawnInterval;
            _availablePowerUps.Clear();

            StartCoroutine(nameof(SpawnPowerUps));
        }

        public void OnReset(DecorationConfiguration decorationConfiguration)
        {

        }

        private IEnumerator SpawnPowerUps()
        {
            while (true)
            {
                yield return new WaitForSeconds(_interval);
                SpawnPowerup();
            }
        }

        private void SpawnPowerup()
        {
            if (_availablePowerUps.Count >= 2 || _powerups.Count <= 0)
            {
                return;
            }

            SpawnPowerup(Random.Range(0, _powerups.Count));
        }

        private int GetSpawnIndexRandomizer()
        {
            int spawnIndex = Random.Range(0, _spawnLocations.Length);

            if (spawnIndex == _lastSpawnIndex)
            {
                return GetSpawnIndexRandomizer();
            }

            return spawnIndex;
        }

        private int GetSpawnIndex()
        {
            return (_lastSpawnIndex + 2) % _spawnLocations.Length;
        }

        private void SpawnPowerup(int powerUpIndex)
        {
            _lastSpawnIndex = GetSpawnIndex();

            GameObject gameObject = Instantiate(_powerups[powerUpIndex], _spawnLocations[_lastSpawnIndex]);
            gameObject.GetComponent<PowerUps>().OnInitialized(_powerUpAttributes);
            _availablePowerUps.Add(gameObject);
        }

        public void OnPowerUpCollected(GameObject gameObject)
        {
            if (_availablePowerUps != null && _availablePowerUps.Contains(gameObject))
            {
                _availablePowerUps.Remove(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
