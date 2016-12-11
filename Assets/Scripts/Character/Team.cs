using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{

    public static int RAT_TEAM = -1;

    public int team = 1;
    public Color teamColor = Color.red;

    public int batteryValue
    {
        get { return team == 1 ? 1 : -1; }
    }

    public static bool IsSameTeam(GameObject first, GameObject second)
    {
        var firstTeamComponent = first.GetComponent<Team>();
        var secondTeamComponent = second.GetComponent<Team>();

        if (firstTeamComponent == null || secondTeamComponent == null)
        {
            return true;
        }

        if (firstTeamComponent && secondTeamComponent)
        {
            return firstTeamComponent.team == secondTeamComponent.team;
        }
        return false;
    }

}
