using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _acceleration;
    [SerializeField] private MinMaxFloat _accelerationFactorRange;
    [SerializeField] private float _terminalVelocity;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _bounceForceMultiplier;
    [SerializeField] private LayerMask _collisionLayers;

    private Vector2 _velocity;
    private CircleCollider2D _collider;
    private const float SkinWidth = 0.03f;
    
    private void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {        
        UpdateRotation();
        UpdateMovement();
        UpdateTranslation();
    }
    private void UpdateRotation()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.Rotate(-transform.forward, horizontal * _rotationSpeed * Time.deltaTime);
    }
    private void UpdateMovement()
    {
        _velocity += Vector2.down * _gravity * Time.deltaTime;
        float vertical = Mathf.Clamp01(Input.GetAxisRaw("Vertical"));
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

}