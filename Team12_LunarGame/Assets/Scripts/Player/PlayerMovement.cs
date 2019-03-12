using Rewired;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _gravity = 8;
    [SerializeField] private float _acceleration = 40;
    [SerializeField] private MinMaxFloat _accelerationFactorRange;
    [SerializeField] private float _terminalVelocity = 15;
    [SerializeField] private float _rotationSpeed = 250;
    [SerializeField] private float _bounceForceMultiplier = 0.5f;
    [SerializeField] private float _playerBounceMultiplier = 1.5f;
    [SerializeField] private LayerMask _collisionLayers = default(LayerMask);
    public int RewiredId = 0;
    [SerializeField] private Vector2 _velocity;
    private CircleCollider2D _collider;    
    private const float SkinWidth = 0.03f;
    private Player _rewiredPlayer;
    
    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _rewiredPlayer = ReInput.players.GetPlayer(RewiredId);
    }
    private void Update()
    {
        UpdateRotation();
        UpdateMovement();
        UpdateTranslation();
    }
    private void UpdateRotation()
    {
        //TODO (Per): add acceleration
        float horizontal = _rewiredPlayer.GetAxisRaw("Horizontal");
        transform.Rotate(-transform.forward, horizontal * _rotationSpeed * Time.deltaTime);
    }
    private void UpdateMovement()
    {
        _velocity += Vector2.down * _gravity * Time.deltaTime;
        float vertical = Mathf.Clamp01(_rewiredPlayer.GetAxisRaw("Vertical"));
        float acceleration = _acceleration * _accelerationFactorRange.Lerp(1 - Vector2.Dot(transform.up, _velocity.normalized));
        _velocity += (Vector2) transform.up * vertical * acceleration * Time.deltaTime;
        _velocity = Vector2.ClampMagnitude(_velocity, _terminalVelocity);
    }
    private void UpdateTranslation()
    {
        Vector2 preHitVelocity = _velocity;
        RaycastHit2D hit = PhysicsHelper.ApplyNormalForce(Cast, Snap, ref _velocity);

        if (hit.normal != Vector2.zero)
        {
            Vector2 bounceDirection = hit.normal;
            Vector2 bounceVelocity = preHitVelocity - _velocity;
            float force = Vector2.Dot(hit.normal, bounceVelocity) * -1f;
            _velocity += bounceDirection.normalized * force * _bounceForceMultiplier;
            //Handle collision with other player
            PlayerMovement otherPlayer = hit.collider.GetComponent<PlayerMovement>();
            if (otherPlayer != null && Vector2.Dot(preHitVelocity.normalized, hit.normal) < 0.0f)
                CollisionManager.HandlePlayerCollision(otherPlayer, preHitVelocity.normalized * Vector2.Dot(preHitVelocity, hit.normal) * -1f * _playerBounceMultiplier);
        }
        
        transform.position += (Vector3) _velocity * Time.deltaTime;
    }
    private RaycastHit2D Cast()
    {
        return Physics2D.CircleCast(transform.position, _collider.radius, _velocity.normalized, _velocity.magnitude * Time.deltaTime + SkinWidth, _collisionLayers);
    }
    private void Snap(RaycastHit2D hit)
    {
        if (hit.normal == Vector2.zero) return;
        transform.position = hit.centroid + hit.normal * SkinWidth;
    }
    public void AddVelocity(Vector2 velocity)
    {
        _velocity += velocity;
    }
}