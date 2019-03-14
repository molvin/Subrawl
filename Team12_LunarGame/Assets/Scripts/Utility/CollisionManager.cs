using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
   private static CollisionManager _instance;

   private bool _waitOneFrame;
   private readonly Dictionary<PlayerMovement, Vector2> _playerCollisionEvents = new Dictionary<PlayerMovement, Vector2>();
   
   private void Awake()
   {
      if(_instance == null)
         _instance = this;
   }
   
   public static void HandlePlayerCollision(PlayerMovement collidedWith, Vector2 velocity)
   {
      if(!_instance._playerCollisionEvents.ContainsKey(collidedWith))
         _instance._playerCollisionEvents.Add(collidedWith, velocity);
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

      foreach (KeyValuePair<PlayerMovement, Vector2> collisionEvent in _playerCollisionEvents)
      {
         collisionEvent.Key.AddVelocity(collisionEvent.Value);
         Debug.Log(collisionEvent.Key.name + " : " + collisionEvent.Value);
      }
      _playerCollisionEvents.Clear();
   }
}
