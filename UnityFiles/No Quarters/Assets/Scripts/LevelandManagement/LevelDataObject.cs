using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Level Data", order = 1)]
public class LevelDataObject : ScriptableObject
{
    //Max amount of enemies in a level
    public int maxEnemies;

    //Max number of enemys in each quadrant
    public int[] maxEnemyInQuad = new int[4];

    //Current number of enemies in each quadrant
    public int[] currentEnemyQuad = new int[4];

    //Current number of enemies in level
    public int currentTotalEnemies;

    //Max enemy value in each quadrant
    public int[] maxEnemyValueInQuad = new int[4];

    //Current enemy value in each quadrant
    public int[] currentEnemyValueInQuad = new int[4];

    //This keeps track of the current enemy types of each type
    public int[] currentEnemyTypeAmount = new int[4];

    //This Keeps track of the maximum amounts of enemy types in each level period. 0 index is commoner, 1 = criminal, 2 = guard, 3 = merchant.
    public int[] maxEnemyTypeAmount = new int[4];
}
