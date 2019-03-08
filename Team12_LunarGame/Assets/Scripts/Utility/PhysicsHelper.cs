using UnityEngine;

public static class PhysicsHelper
{
    public delegate RaycastHit2D RaycastDelegate();
    public delegate void SnappingDelegate(RaycastHit2D hit);
    private const int MaxIterations = 50;
    
    public static RaycastHit2D ApplyNormalForce(RaycastDelegate rayCastMethod, SnappingDelegate snappingMethod, ref Vector2 velocity)
    {
        RaycastHit2D finalHit = new RaycastHit2D();
        
        for (int i = 0; i < MaxIterations; i++)
        {            
            RaycastHit2D hit = rayCastMethod();
            if (hit.normal == Vector2.zero) return finalHit;

            finalHit = hit;
            snappingMethod(hit);
            velocity += hit.normal * -1f * Vector2.Dot(hit.normal, velocity);
        }
        return finalHit;
    }
    
}