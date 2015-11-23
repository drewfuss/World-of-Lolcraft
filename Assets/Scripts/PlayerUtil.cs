using UnityEngine;
using System.Collections;

public class PlayerUtil {
    public static int strength;
    public static int level;
    private static readonly float[] player_xp = {100f, 200f, 300f, 400f, 500f};

    public PlayerUtil()
    {

        
    }
    public static void UpdateStrength()
    {
        strength *= level;
    }
    public static float GetMaxExperience(int index)
    {
        return player_xp[index];
    }

}
