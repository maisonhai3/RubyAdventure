using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    #region variables

    public WeaponType weaponType;
    public Flame flameColor;

    private IWeapon iWeapon;
    private IFlame iFlame;

    #endregion

    private void Start()
    {
        ChangeWeapon(); //to check the value of weaponType in the inspector initially
        ChangeFlameColor();
        iFlame.ShowFlame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Fire();

        //to check the value of weaponType in the inspector while in play mode
        if (Input.GetKeyDown(KeyCode.C)) 
            ChangeWeapon();

        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeFlameColor();
            iFlame.ShowFlame();
        }
    }

    private void ChangeWeapon()
    {
        //To prevent Unity from creating multiple copies of the same component in inspector at runtime
        var c = gameObject.GetComponent<IWeapon>() as Component;

        if (c != null) Destroy(c);

        // The Strategy Pattern
        iWeapon = weaponType switch
        {
            WeaponType.Missile => gameObject.AddComponent<Missile>(),
            WeaponType.Bullet => gameObject.AddComponent<Bullet>(),
            _ => gameObject.AddComponent<Bullet>()
        };
    }

    public void ChangeFlameColor()
    {
        var c = gameObject.GetComponent<IFlame>() as Component;

        if (c != null)
        {
            Destroy(c);
            iFlame.DestroyFlame(); // so that number of flame objects remains one
        }

        // The Strategy Pattern
        iFlame = flameColor switch
        {
            Flame.Blue => gameObject.AddComponent<BlueFlame>(),
            Flame.Red => gameObject.AddComponent<RedFlame>(),
            _ => gameObject.AddComponent<BlueFlame>()
        };
    }

    public void Fire()
    {
        iWeapon.Shoot();
    }
}

public enum WeaponType
{
    Missile,
    Bullet
}

public enum Flame
{
    Blue,
    Red
}