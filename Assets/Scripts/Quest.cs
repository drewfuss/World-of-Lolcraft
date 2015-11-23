using UnityEngine;
using System.Collections;

public class Quest  {

    private int id;
    private int xp;
    private string name;
    private int max_kill;
    private string enemy_name;
    private int current_kill;
    private bool completed;
    private bool kill_quest;
    private string key;
    private int amount;
    private int c_quant;
    private string quest_giver;

    public Quest(int id, string name, string enemy_name, int max_kill, int xp)
    {
        this.id = id;
        this.name = name;
        this.enemy_name = enemy_name;
        this.max_kill = max_kill;
        this.xp = xp;
        current_kill = 0;
        kill_quest = true;
    }

    public Quest(int id, string name, string enemy_name, int xp)
    {
        this.id = id;
        this.name = name;
        this.enemy_name = enemy_name;
        this.xp = xp;

        kill_quest = false;
    }

    public bool killType()
    {
        return kill_quest;
    }

    public int xpType()
    {
        return xp;
    }
    public string getName()
    {
        return name;
    }
    public int currentKill()
    {
        return current_kill;
    }
    public int maxKill()
    {
        return max_kill;
    }
    public void setCollection(int amount, string item)
    {
        this.amount = amount;
        this.key = item;
    }
    public string getEnemyName()
    {
        return enemy_name;
    }
    public bool IsFinished()
    {
        if (kill_quest)
        {
            if (current_kill == max_kill)
            {
                completed = true;
                return completed;
            }
        }
        else
        {
            if(c_quant == amount)
            {
                completed = true;
                return completed;
            }
        }
        return false;
    }
    public string getCrit()
    {
        if (kill_quest == true)
        {
            return current_kill.ToString() + "/" + max_kill.ToString() + " : " + enemy_name;
        }
        return "Testing";
    }
    public void AddKill()
    {
        current_kill++;
        
    }
	
}
