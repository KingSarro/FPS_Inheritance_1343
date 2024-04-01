using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Gun{
    [SerializeField] GameObject shotgunBlastRef;
   public override bool AttemptFire()
    {
        if (!base.AttemptFire())
            return false;

        var b = Instantiate(bulletPrefab, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);
        b.GetComponent<Projectile>().Initialize(10, 100, 0.5f, 25, null); // version without special effect

        Instantiate(shotgunBlastRef, gunBarrelEnd.transform.position, gunBarrelEnd.rotation);

        anim.SetTrigger("shoot");
        elapsed = 0;
        ammo -= 1;

        return true;
    }
}
