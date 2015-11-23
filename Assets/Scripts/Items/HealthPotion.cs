using UnityEngine;
using System.Collections;

public class HealthPotion : Item {

    public HealthPotion(int healing, int strength, int combo_points, int drop_rate)
        :base(healing, strength, combo_points, drop_rate)
    {
        name = "Healing Potion";
    }



}
