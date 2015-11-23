using UnityEngine;
using System.Collections;

public class Item  {

    protected string name;
    protected int healing;
    protected int strength;
    protected int combo_points;
    protected int drop_rate;
    protected int[] stats;
    public int quanitity;

    protected Item(int healing, int strength, int combo_points, int drop_rate)
    {
        this.healing = healing;
        this.strength = strength;
        this.combo_points = combo_points;
        this.drop_rate = drop_rate;
        stats = new int[4];
        stats[0] = healing;
        stats[1] = strength;
        stats[2] = combo_points;
        stats[3] = drop_rate;

    }

    public string GetName()
    {
        return name;
    }
    public int[] GetStats()
    {
        return stats;
    }
    public void addQuantity(int num)
    {
        quanitity += num;
    }
    public int getQuantity()
    {
        return quanitity;
    }
    public int getDrop()
    {
        return drop_rate;
    }
    

}
