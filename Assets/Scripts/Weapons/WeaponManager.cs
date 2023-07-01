using UnityEngine;
/// <summary>
/// Class <c>WeaponManager</c> controls the queue, assigns power ups, checks for shots.
/// </summary>
public class WeaponManager : MonoBehaviour
{
    //Defines a node in the dynamic queue
    private class Node
    {
        //modifica stupida
        public Weapon Weapon { get; set; }
        public Node Next { get; set; }
        public Node(Weapon _weapon)
        {
            this.Weapon = _weapon;
            this.Next = null;
        }

        public Node(Weapon _weapon, Node _previous)
        {
            this.Weapon = _weapon;
            this.Next = null;
            _previous.Next = this;
        }
    }
}
