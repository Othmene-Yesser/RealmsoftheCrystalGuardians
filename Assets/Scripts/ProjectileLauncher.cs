using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position,projectilePrefab.transform.rotation);
        Vector3 origscale = projectile.transform.localScale;

        //flip el bow projectile depends on the facing direction of the player
        projectile.transform.localScale = new Vector3(
            origscale.x * transform.localScale.x>0?1:-1,
            origscale.y,
            origscale.z
            );
    }
}
