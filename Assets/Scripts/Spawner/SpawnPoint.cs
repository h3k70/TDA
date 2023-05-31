using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Enemy Spawn(Enemy template)
    {
        return Instantiate(template, transform);
    }
}
