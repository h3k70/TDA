using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //[SerializeField] private float _deley = 1;

    //private float _timeAfterLastSpawn;
    //private bool _isReady = true;

    //bool IsReady => _isReady;

    public Enemy Spawn(Enemy template)
    {
        //_isReady = false;
        //_timeAfterLastSpawn = 0;
        //StartCoroutine(Preparation());
        return Instantiate(template, transform);
    }

    /*private IEnumerator Preparation()
    {
        while(_timeAfterLastSpawn < _deley)
        {
            _timeAfterLastSpawn += Time.deltaTime;
        }
        _isReady = true;

        yield break;
    }
    */
}
