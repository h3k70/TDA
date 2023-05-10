using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Building _target;
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();
    [SerializeField] private List<Wave> _waves;

    private Wave _currentWave;
    private int _currentWaveNumber = -1;
    private Blowout _currentBlowout;
    private float _timeAfterLastSpawn;
    private int _countSpawnedInBlowout;
    private int _allCountSpawned;
    private int _numberOfKilled;
    private bool _IsAllSpawned = false;

    public event UnityAction AllEnemyKilled;

    public void StartNextWave()
    {
        if((_waves.Count - 1) > _currentWaveNumber)
        {
            _currentWaveNumber++;
            _currentWave = _waves[_currentWaveNumber];
            StartCoroutine(WaveSpawning());
        }
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _numberOfKilled++;

        if (_IsAllSpawned == true && _allCountSpawned == _numberOfKilled)
        {
            AllEnemyKilled?.Invoke();
        }
    }

    private void SpawnEnemy(Enemy template)
    {
        var enemy = _spawnPoints[Random.Range(0, _spawnPoints.Count)].Spawn(template);
        enemy.Init(_target);
        enemy.Dying += OnEnemyDying;
        _timeAfterLastSpawn = 0;
        _countSpawnedInBlowout++;
        _allCountSpawned++;
    }

    private IEnumerator WaveSpawning()
    {
        for (int i = 0; i < _currentWave.Blowouts.Count; i++)
        {
            _currentBlowout = _currentWave.Blowouts[i];
            _timeAfterLastSpawn = _currentBlowout.Deley;

            while (_currentBlowout.EnemyCount > _countSpawnedInBlowout)
            {
                _timeAfterLastSpawn += Time.deltaTime;

                if (_timeAfterLastSpawn >= _currentBlowout.Deley)
                {
                    if(_currentBlowout.MultiplicitySpecialEnemy != 0 && ((_countSpawnedInBlowout + 1) % _currentBlowout.MultiplicitySpecialEnemy) == 0)
                        SpawnEnemy(_currentBlowout.SpecialEnemy);
                    else
                        SpawnEnemy(_currentBlowout.Enemy);
                }
                yield return null;
            }
            _countSpawnedInBlowout = 0;
            yield return new WaitForSeconds(_currentWave.Blowouts[i].DelayBeforeNextBlowout);
        }
        _IsAllSpawned = true;
        yield break;
    }
}

[System.Serializable]
public class Wave
{
    public List<Blowout> Blowouts;
}

[System.Serializable]
public class Blowout
{
    public Enemy Enemy;
    public float Deley;
    public Enemy SpecialEnemy;
    public int MultiplicitySpecialEnemy;
    public int EnemyCount;
    public float DelayBeforeNextBlowout;
}