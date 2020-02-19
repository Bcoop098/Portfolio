using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelLord : MonoBehaviour
{
    //public static PixelLord pixelLordInstance;

    [SerializeField] GameObject[] quadrants;
    [SerializeField] GameObject quadrantPrefab;

    //Max amount of enemies in a level
    [SerializeField] int maxEnemies;

    //Max number of enemys in each quadrant
    [SerializeField] int[] maxEnemyInQuad = new int [4];

    //Current number of enemies in each quadrant
    [SerializeField] int[] currentEnemyQuad = new int[4];

    //Current number of enemies in level
    [SerializeField] int currentTotalEnemies;

    //Max enemy value in each quadrant
    [SerializeField] int[] maxEnemyValueInQuad = new int[4];

    //Current enemy value in each quadrant
    [SerializeField] int[] currentEnemyValueInQuad = new int[4];

    //This keeps track of the current enemy types of each type
    [SerializeField] int[] currentEnemyTypeAmount = new int[4];

    //This Keeps track of the maximum amounts of enemy types in each level period. 0 index is commoner, 1 = criminal, 2 = guard, 3 = merchant.
    [SerializeField] int[] maxEnemyTypeAmount = new int[4];

    //current quadrant player is in
    [SerializeField] int currentPlayerQuad;

    //Keeps track of current index in enemy pool. mostly used when populating it
    int indexInPool = 0;

    [SerializeField] List<string> enemyPoolStatus = new List<string>();
    [SerializeField] List<GameObject> enemyPool = new List<GameObject>();

    GameObject enemy;

    GameObject[] enemyTypes = new GameObject[4];//Array containging the all enemytypes.

    //Transforms/Positions
    Transform[] QEnemyPt = new Transform[4];//Array containing the spawnpoints of all enemies

    bool gameStarted = false;

    //Functions
    public void UpdatePlayerQuadrant(int quadID)
    {
        switch (quadID)
        {
            case 0:
                currentPlayerQuad = quadID;
                break;
            case 1:
                currentPlayerQuad = quadID;
                break;
            case 2:
                currentPlayerQuad = quadID;
                break;
            case 3:
                currentPlayerQuad = quadID;
                break;
        }

    }

    public void EnemyKilled(int iD, int quadrant)
    {
        enemyPool[iD].SetActive(false);
        enemyPoolStatus[iD] = "inactive";
        currentEnemyQuad[quadrant] -= 1;
        currentTotalEnemies--;
        currentEnemyValueInQuad[quadrant] -= enemyPool[iD].GetComponent<BaseHuman>().GetCoinAmt();
        Toolbox.Instance.GetObject<AnalysisManager>("AnalysisManager").KillAnEnemy();
    }

    //Getters--------------------------------------------------------
    public int GetPlayerQuadrant() { return currentPlayerQuad; }

    public Vector3 GetNextPlayerQuadrant()
    {
        if (currentPlayerQuad + 1 >= 4)
        {
            return quadrants[currentPlayerQuad].transform.position;
        }
        return quadrants[(currentPlayerQuad + 1)].transform.position;
    }

    public Vector3 GetCurrentPlayerQuadrant()
    {
        if(currentPlayerQuad -1 < 0)
        {
            return quadrants[(0)].transform.position;
        }
        return quadrants[(currentPlayerQuad -1)].transform.position;
    }

    public Vector3 GetPreviousPlayerQuadrant()
    {
        if (currentPlayerQuad - 1 <= 0)
        {
            return quadrants[currentPlayerQuad].transform.position;
        }
        return quadrants[(currentPlayerQuad - 1)].transform.position;
    }

    //Setters--------------------------------------------------------
    public void SetPlayerQuadrant(int newQuad) { currentPlayerQuad = newQuad; }

    public void SetGameStatus(bool status) { gameStarted = status; }

    //Always running... ALways watching
    //Quadrant 1 
    private void MonitorQuadOne()
    {
        SpawnEnemies(0);
    }

    //Quadrant 2 
    private void MonitorQuadTwo()
    {
        SpawnEnemies(1);
    }

    //Quadrant 3 
    private void MonitorQuadThree()
    {
        SpawnEnemies(2);
    }

    //Quadrant 4
    private void MonitorQuadFour()
    {
        SpawnEnemies(3);
    }

    //This function will take in what quad the we want to spawn in and it will spawn an enemy at a time.
    private void SpawnEnemies(int quadNum)
    {
        int index = Random.Range(0, 3);
        int tmpIndex = 0;

        //This is to prevent index from going to far if there are certain types of enemies not available in that level
        do
        {
            index = Random.Range(0, 3);
          
        } while (maxEnemyTypeAmount[index] <= 0);

        if (currentEnemyQuad[quadNum] < maxEnemyInQuad[quadNum] && currentPlayerQuad != quadNum && enemyPool != null)
        {
            //Generate where in the enemypool to start the index, more for efficiency sake.
            tmpIndex = GenerateIndexLocationOfEnemyType(index);
            
            for (int currentIndex = tmpIndex; currentEnemyQuad[quadNum] <= maxEnemyInQuad[quadNum] && currentEnemyValueInQuad[quadNum] <= maxEnemyValueInQuad[quadNum]; currentIndex++)
            {
                //if by some reason the index is greater than the tmp, break. Meaning if I was looking for all peasants and I now move past into a different enemy
                //type in the array, than I am no longer searching for peasants break out. Or if its just about to go out of range
                if (currentIndex >= maxEnemies)
                {
                    break;
                }

                if (enemyPoolStatus[currentIndex] == "inactive" && enemyPool[currentIndex].GetComponent<BaseHuman>().GetTypeID() == index)
                {
                    if (currentEnemyValueInQuad[quadNum] + enemyPool[currentIndex].GetComponent<BaseHuman>().GetCoinAmt() <= maxEnemyValueInQuad[quadNum] &&
                           currentEnemyQuad[quadNum] + 1 <= maxEnemyInQuad[quadNum] && currentTotalEnemies + 1 <= maxEnemies)
                    {

                        //Turn the object on
                        enemyPool[currentIndex].transform.position = QEnemyPt[quadNum].position + new Vector3(Random.Range(-6f, 6f), Random.Range(-2.5f, 2.5f),0);
                        enemyPool[currentIndex].GetComponent<BaseHuman>().SetQuadrant(quadNum);
                        enemyPool[currentIndex].SetActive(true);
                        enemyPoolStatus[currentIndex] = "active";

                        //Acknowledge the "spawned" enemy                           
                        currentEnemyQuad[quadNum] += 1;

                        //update the current value of all enemies in quad
                        currentEnemyValueInQuad[quadNum] += enemyPool[currentIndex].GetComponent<BaseHuman>().GetCoinAmt();
                        currentTotalEnemies += 1;

                        break;
                    }
                 

                }
            }     
        }
    }

    /*Purpose: To take in info of what enemy type are we looking for? 0 = peasant, 1 = criminal, 2 = guard, 3 = merchant.
     * Task: To take in the info and find how many enemies of that type are in the game. Then return that number.
     * This number will essentially start the search in the enemypool at the correct location
     */
    private int GenerateIndexLocationOfEnemyType(int enemyType)
    {
        switch (enemyType)
        {
            case 0:
                return 0;

            case 1:
                return maxEnemyTypeAmount[enemyType - 1];

            case 2:
                return maxEnemyTypeAmount[enemyType - 1] + maxEnemyTypeAmount[enemyType - 2];


            case 3:
                return maxEnemyTypeAmount[enemyType - 1] + maxEnemyTypeAmount[enemyType - 2] + maxEnemyTypeAmount[enemyType - 3];
        }
        return 0;
    }
    private void InitializeQuadsArrays()
    {
        //2D array that will hold the quad id as row and then the 2nd index as number of enemies in said quad.
        currentEnemyQuad = new int[4];

        //Initializes the enemy quadrants list
        for (int i = 0; i < 4; i++)
        {
            currentEnemyQuad[i] = 0;
        }
    }

    private void CreateEnemyPool()
    {
        //Populate with Commoners
        PopulateEnemyPool(maxEnemyTypeAmount[0], 0);
        //Populate with Criminals
        PopulateEnemyPool(maxEnemyTypeAmount[1], 1);
        //Populate with Guards
        PopulateEnemyPool(maxEnemyTypeAmount[2], 2);
        //Populate with Merchants
        //PopulateEnemyPool(maxEnemyTypeAmount[3],3);
    }

    private void PopulateEnemyPool(int maxOfType, int index)
    {
        //Add to enemy pool the total enemies for a level and puts it in the disabled pool
        for (int i = indexInPool; i < maxEnemies; i++)
        {

            if (currentEnemyTypeAmount[index] < maxEnemyTypeAmount[index])
            {
                enemyPool.Add(enemyTypes[index]);
                enemyPool[i] = Instantiate(enemyPool[i]);
                enemyPool[i].SetActive(false);
                enemyPoolStatus.Add("inactive");
                switch(index)
                {
                        //Peasant
                    case 0:
                        enemyPool[i].GetComponent<PeasantScriptV2>().SetID(i);
                        break;

                        //Criminal
                    case 1:
                        enemyPool[i].GetComponent<CriminalScriptV2>().SetID(i);
                        break;

                        //Guard
                    case 2:
                        enemyPool[i].GetComponent<GuardAIScript>().SetID(i);
                        break;

                        //Merchant
                    case 3:
                       // enemyPool[i].GetComponent<MerchantAISC>().SetID(i);
                        break;
                }
        
                indexInPool++;

                currentEnemyTypeAmount[index]++;
            }
        }
    }

    //Clean up the pool
    public void ResetLevelData()
    {
        currentPlayerQuad = 0;
        indexInPool = 0;
        enemyPool.Clear();
        enemyPoolStatus.Clear();
        currentPlayerQuad = 0;
        currentTotalEnemies = 0;
        for(int i = 0; i < maxEnemyInQuad.Length; i++)
        {
            currentEnemyQuad[i] = 0;
            currentEnemyTypeAmount[i] = 0;
            currentEnemyValueInQuad[i] = 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //pixelLordInstance = this;
        quadrantPrefab = Resources.Load("Quadrant") as GameObject;

        enemy = Resources.Load("GuardPrefab") as GameObject;

        //load all the enemy types and store them into the array
        enemyTypes[0] = Resources.Load("PeasantPrefab") as GameObject;
        enemyTypes[1] = Resources.Load("CriminalPrefab") as GameObject;
        enemyTypes[2] = Resources.Load("GuardPrefab") as GameObject;
        enemyTypes[3] = Resources.Load("MerchantPrefab") as GameObject;

        //This sets the initial zone player is in to 0(aka first one)
        currentPlayerQuad = 0;

        GameObject tmp;
        tmp = Resources.Load("EnemySpawns") as GameObject;

        QEnemyPt[0] = tmp.transform.GetChild(0).transform;
        QEnemyPt[1] = tmp.transform.GetChild(1).transform;
        QEnemyPt[2] = tmp.transform.GetChild(2).transform;
        QEnemyPt[3] = tmp.transform.GetChild(3).transform;

        //This function call will initialize all the data related arrays such as enemytypes, enemyvalues in a quad, etc..
        InitializeQuadsArrays();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(gameStarted)
        {   
            MonitorQuadOne();
            MonitorQuadTwo();
            MonitorQuadThree();
            MonitorQuadFour();
        }
        
    }

    public void InitializeEnemyData()
    {
        //pixelLordInstance = this;
        quadrantPrefab = Resources.Load("Quadrant") as GameObject;

        enemy = Resources.Load("GuardPrefab") as GameObject;

        //load all the enemy types and store them into the array
        enemyTypes[0] = Resources.Load("PeasantPrefab") as GameObject;
        enemyTypes[1] = Resources.Load("CriminalPrefab") as GameObject;
        enemyTypes[2] = Resources.Load("GuardPrefab") as GameObject;
        enemyTypes[3] = Resources.Load("MerchantPrefab") as GameObject;

        //This sets the initial zone player is in to 0(aka first one)
        currentPlayerQuad = 0;

        GameObject tmp;
        tmp = Resources.Load("EnemySpawns") as GameObject;

        QEnemyPt[0] = tmp.transform.GetChild(0).transform;
        QEnemyPt[1] = tmp.transform.GetChild(1).transform;
        QEnemyPt[2] = tmp.transform.GetChild(2).transform;
        QEnemyPt[3] = tmp.transform.GetChild(3).transform;

        //This function call will initialize all the data related arrays such as enemytypes, enemyvalues in a quad, etc..
        InitializeQuadsArrays();
    }

    public void SetData(LevelDataObject incomingREE)
    {
        ResetLevelData();

        maxEnemies = incomingREE.maxEnemies;
        maxEnemyInQuad = incomingREE.maxEnemyInQuad;
        maxEnemyTypeAmount = incomingREE.maxEnemyTypeAmount;
        maxEnemyValueInQuad = incomingREE.maxEnemyValueInQuad;

        currentEnemyQuad = incomingREE.currentEnemyQuad;
        currentEnemyTypeAmount = incomingREE.currentEnemyTypeAmount;
        currentEnemyValueInQuad = incomingREE.currentEnemyValueInQuad;

  
        CreateEnemyPool();

        gameStarted = true;

        quadrants = new GameObject[3];
        int index = 1;
        for (int i = 0; i < quadrants.Length; i++)
        {
            quadrants[i] = GameObject.Find("Quadrant " + "(" + index.ToString() + ")");
            index += 2;
        }
       
    }
}
