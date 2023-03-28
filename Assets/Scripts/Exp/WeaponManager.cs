using UnityEngine;

public class WeaponManager : MonoBehaviour
{
}

public interface IWeapon
{
    void Shoot();
    
    // Add Destroy the bullet method.
}

public class Bullet : MonoBehaviour, IWeapon
{
    public void Shoot()
    {
        var initialPosition = new Vector3(transform.position.x, transform.position.y + 1f, 0);
        var bullet = Instantiate(Resources.Load("BulletPrefab", typeof(GameObject))) as GameObject;
        bullet.transform.position = initialPosition;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f);
    }
}

public class Missile : MonoBehaviour, IWeapon
{
    public void Shoot()
    {
        var initialPosition = new Vector3(transform.position.x, transform.position.y + 1f, 0);
        var missile = Instantiate(Resources.Load("MissilePrefab", typeof(GameObject))) as GameObject;
        missile.transform.position = initialPosition;
        missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 3f);
    }
}