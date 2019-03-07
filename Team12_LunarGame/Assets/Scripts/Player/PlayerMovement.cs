using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _gravity;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _terminalVelocity;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private LayerMask _collisionLayers;

    private Vector3 _velocity;
    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        //Gravity
        _velocity += Vector3.down * _gravity * Time.deltaTime;
        //Acceleration        
        float vertical = Input.GetAxisRaw("Vertical");
        _velocity += transform.up * vertical * _acceleration * Time.deltaTime;
        _velocity = Vector2.ClampMagnitude(_velocity, _terminalVelocity);
        //Rotation
        float horizontal = Input.GetAxisRaw("Horizontal");
        transform.Rotate(-transform.forward, horizontal * _rotationSpeed * Time.deltaTime);
        //Translation
        UpdateCollision();
        transform.position += _velocity * Time.deltaTime;
    }

    private void UpdateCollision()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3) _collider.offset, _collider.bounds.size, transform.rotation.eulerAngles.z, _velocity.normalized, _velocity.magnitude * Time.deltaTime, _collisionLayers);
        if (hit.normal == Vector2.zero) return;
        _velocity += (Vector3) hit.normal * -1f * Vector2.Dot(hit.normal, _velocity);
    }

}


