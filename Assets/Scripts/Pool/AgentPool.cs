using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPool : MonoBehaviour
{
    public static AgentPool _agentPool;

    public AgentManager agentPrefab;

    public List<AgentManager> agentPool;

    public int agentCountAtStart = 10;
    public int agentAliveCount = 0;
    public int livingAgents = 0;

    public List<Transform> agentCoordinates;
    public GameObject agentCoordinateParent;

    public GameObject endGameParent;
    public List<Transform> endGamePositions;

    private void Awake()
    {
        endGamePositions = new List<Transform>();
        for (int i = 0; i < endGameParent.transform.childCount; i++)
        {
            endGamePositions.Add(endGameParent.transform.GetChild(i));
        }

        _agentPool = this;

        agentPool = new List<AgentManager>();
        GetCoordinates();

        for (int i = 0; i < agentCountAtStart; i++)
        {
            AgentManager newAgent = Instantiate(agentPrefab);
            newAgent.transform.parent = transform.parent.transform;

            Vector3 worlPos = agentCoordinates[i].position;
            agentAliveCount++;
            newAgent.Init(worlPos);

            newAgent.gameObject.SetActive(false);
            agentPool.Add(newAgent);
        }

        CreateEnemy();
    }

    public bool _started;

    void Start()
    {
        _started = true;
    }

    void OnEnable()
    {
        AgentManager.Complete += SendToCoordinates;
    }

    void OnDisable()
    {
        AgentManager.Complete -= SendToCoordinates;
    }

    AgentManager GetEnemyFromPool()
    {
        for (int i = 0; i < agentPool.Count; i++)
        {
            if (!agentPool[i].gameObject.activeSelf)
            {
                return agentPool[i];
            }
        }
        return null;
    }

    public void CreateEnemy()
    {
        AgentManager newAgent = GetEnemyFromPool();

        if(newAgent == null)
        {
            print("MaxAgentReached!");
            return;
        }


        newAgent.gameObject.SetActive(true);
        livingAgents++;
        agentAliveCount++;

    }

    void GetCoordinates()
    {
        agentCoordinates = new List<Transform>();

        int childCount = 0;

        childCount = agentCoordinateParent.transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            agentCoordinates.Add(agentCoordinateParent.transform.GetChild(i));
        }
    }

    public void SendToCoordinates()
    {

        for (int i = 0; i < agentPool.Count; i++)
        {
            agentPool[i].GetTarget(endGamePositions[i].position);
        }

    }
}
