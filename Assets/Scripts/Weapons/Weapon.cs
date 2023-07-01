using UnityEngine;

/// <summary>
/// Class <c>Weapon</c> defines the characteristics for a weapon. All the weapons are considered "ranged" -> Red is a ranged weapon with range 0 (melee)
/// </summary>
public class Weapon : MonoBehaviour
{

    readonly public int totCharges = 50; //like in LoL each weapon starts with a finite number of projectiles. There is no reload dynamic. 
    public int current_charges { get; private set; }

    //properties of the weapon, because there could be "power ups" to change some values.
    public int damage { get; protected set; }           //how much danage the weapon deals -> assign this to the PROJECTILE INSTEAD? -> damage depends on target hit
    public int range { get; protected set; }            // how far can the weapon shoot
    public int timeBetweenShots { get; protected set; } //inter-shot cooldown

}
