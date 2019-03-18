using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissileManager : MonoBehaviour
{
    [SerializeField]private GameObject missilePrefab = null;
    [SerializeField]private int missileCount = 5;
    [SerializeField] private float missileSpeed = 10f;
    [SerializeField] private float velocitySmoothTime = 0.1f;
    [SerializeField] private LayerMask collisionLayers;
    [SerializeField] private float MissileLifeTime = 2f;
    private List<Missile> activeMissiles = new List<Missile>();
    private PickupManager pickUpManagerReference = null;
    private int SpawnCounter = 0;
    private List<int> missileIdToDestroy = new List<int>();


    private class Missile
    {
        public GameObject MissileObj;
        public int OwnerID;
        public int TargetID;
        public Vector2 Velocity;
        public CircleCollider2D Collider;
        public int SpawningID;
        public float LifeTimeConter = 0.0f;
    }
    void Start()
    {
        pickUpManagerReference = FindObjectOfType<PickupManager>();
    }

    void Update()
    {
        for(int i = 0; i < activeMissiles.Count; ++i)
        {
            activeMissiles[i].LifeTimeConter += Time.deltaTime;
            if(activeMissiles[i].LifeTimeConter > MissileLifeTime)
            {
                missileIdToDestroy.Add(activeMissiles[i].SpawningID);
            }
            Transform targetTransform = PlayerValues.GetPlayer(activeMissiles[i].TargetID)?.transform;
            if (targetTransform == null)
                continue;
            Vector2 targetDir = targetTransform.position - activeMissiles[i].MissileObj.transform.position;
            Vector2 velocityDelta = Vector2.zero; 
            activeMissiles[i].Velocity = Vector2.SmoothDamp(activeMissiles[i].Velocity,
                (targetDir).normalized * missileSpeed, ref velocityDelta, velocitySmoothTime);
            activeMissiles[i].MissileObj.transform.position += (Vector3)activeMissiles[i].Velocity * Time.deltaTime;

            RaycastHit2D hit = Physics2D.CircleCast(activeMissiles[i].MissileObj.transform.position, activeMissiles[i].Collider.radius, activeMissiles[i].Velocity.normalized, activeMissiles[i].Velocity.magnitude * Time.deltaTime, collisionLayers);
            if(hit.collider != null)
            {
                PlayerValues Player = hit.collider.GetComponent<PlayerValues>();
                
                if(Player != null && Player.Id != activeMissiles[i].OwnerID)
                {
                    Player.Die();
                    missileIdToDestroy.Add(activeMissiles[i].SpawningID);
                }
            }
        }
        for(int i  = activeMissiles.Count - 1; i >= 0; i--)
        {
            if(missileIdToDestroy.Contains(activeMissiles[i].SpawningID))
            {
                Destroy(activeMissiles[i].MissileObj);
                activeMissiles.RemoveAt(i);
            }
        }
        missileIdToDestroy.Clear();
    }
    public void Fire()
    {
        
        int ownerID = pickUpManagerReference.GetCurrentPickupOwner();
        PlayerValues Target = PlayerValues.GetPlayer(ownerID == 0 ? 1 : 0);
        for(int i = 0; i < missileCount; ++i)
        {
            GameObject instance = Instantiate(missilePrefab, PlayerValues.GetPlayer(ownerID).transform.position, Quaternion.identity);
            Vector2 startVelocity = Quaternion.Euler(0,0, Random.Range(0, 360))*(Vector2.up * missileSpeed);
            activeMissiles.Add(new Missile { MissileObj = instance, OwnerID = ownerID, TargetID = Target.Id, Velocity = startVelocity, Collider = instance.GetComponent<CircleCollider2D>(), SpawningID = SpawnCounter });
        }
        SpawnCounter++;
    }
}
