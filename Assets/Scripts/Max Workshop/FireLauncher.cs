using UnityEngine;


public class FireLauncher : MonoBehaviour
{
    public GameObject projectilePrefab = null;
    public Transform startPoint = null;
    public float projectileSpeed = 500.0f;
    public void Fire()
    {
        GameObject newObject = Instantiate(projectilePrefab, startPoint.position, startPoint.rotation, null);
        // Apply force to the projectile.
        if (newObject.TryGetComponent(out Rigidbody rigidBody))
            rigidBody.AddForce(startPoint.forward * projectileSpeed);
    }
}
