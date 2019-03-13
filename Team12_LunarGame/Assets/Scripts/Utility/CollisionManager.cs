using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
   private static CollisionManager _instance;

   private bool _waitOneFrame;
   private readonly List<Tuple<PlayerMovement, Vector2>> _playerCollisionEvents = new List<Tuple<PlayerMovement, Vector2>>();
   
   private void Awake()
   {
      if(_instance == null)
         _instance = this;
   }
   
   public static void HandlePlayerCollision(PlayerMovement collidedWith, Vector2 velocity)
   {
      if (_instance == null)
      {
         GameObject go = new GameObject("CollisionManager");
         _instance = go.AddComponent<CollisionManager>();
      }
      _instance._playerCollisionEvents.Add(new Tuple<PlayerMovement, Vector2>{First = collidedWith, Second = velocity});
      _instance._waitOneFrame = true;
   }
   private void Update()
   {
      if (_waitOneFrame)
      {
         _waitOneFrame = false;
         return;
      }
      if (_playerCollisionEvents.Count == 0) 
         return;

      foreach (Tuple<PlayerMovement,Vector2> collisionEvent in _playerCollisionEvents)
      {
         collisionEvent.First.AddVelocity(collisionEvent.Second);
      }
      _playerCollisionEvents.Clear();
   }
}
