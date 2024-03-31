
using System;
using Health;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory: MonoBehaviour
{

    
    [Header("Inventory Settings")]
    [SerializeField] private int xp;
    [SerializeField] private int gold;

    [SerializeField] private GameObject[] items;
    [SerializeField] private Camera _spawnAreaCamera;
    
    private EnemySpawner.EnemySpawner _enemySpawner;
    
    public void SetEnemySpawner(GameObject areaController)
    {
        _enemySpawner = areaController.GetComponent<EnemySpawner.EnemySpawner>();

        //We want to get the Model of the player and the only way we can get that is by getting the playerhealth component
        _enemySpawner.playerCamera = _spawnAreaCamera;
        _enemySpawner.playerTransform = GetComponent<PlayerHealth>().transform;
        
        _enemySpawner.OnEnemySpawned += SubscribeToEnemyDeath;
    }

    private void SubscribeToEnemyDeath(GameObject enemy)
    {
        var enemyHealth = enemy.GetComponent<EnemyHealth>();
        SubscribeToEnemyDeath(enemyHealth);
    }
    
    private void SubscribeToEnemyDeath(EnemyHealth enemyHealth)
    {
        enemyHealth.OnDie += OnEnemyDeath;
    }
    
    private void OnEnemyDeath(GameObject deadObject)
    {
        var enemyHealth = deadObject.GetComponent<EnemyHealth>();
        
        gold += enemyHealth.GoldValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("XP"))
        {
            xp += other.GetComponent<FloatObject>().xpValue;
            Destroy(other.gameObject);
        };
    }
}
