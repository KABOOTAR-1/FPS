using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform player;
    float dis;

    bool playerwithsightrange;
    bool playerwithattackrange;

    [SerializeField]
    float sightrange;
    [SerializeField]
    float attackrange;
    public LayerMask playermask;
    [SerializeField]
    bool walkPointSet;
    float walkpointRange;
    Vector3 walkpoint;
    [SerializeField]
    float timebtnattacks;
    public float currtime;
    private void Awake()
    {
        
}
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        dis = 1.5f;
        sightrange = 25f;
        attackrange = 5f;
        playerwithattackrange = false;
        playerwithsightrange = false;
        walkPointSet = false;
        walkpointRange = 10f;
        playermask = LayerMask.GetMask("Player");
        agent.stoppingDistance = dis;
        timebtnattacks = 0.75f;

    }
    bool isFront()
    {
        Vector3 direction=transform.position-player.position;

        float angle = Vector3.Angle(transform.forward, direction);
        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            Debug.DrawLine(transform.position, player.position, Color.red);
            return true;
        }


        return false;
    }
    // Update is called once per frame
    void Update()
    {
        // isFront();
        //agent.destination = player.position;
        playerwithsightrange = Physics.CheckSphere(transform.position, sightrange, playermask);
        playerwithattackrange = Physics.CheckSphere(transform.position, attackrange, playermask);

        if (!playerwithsightrange ||(playerwithsightrange && !isFront()) && !playerwithattackrange) movingAround();
        else if(playerwithsightrange && !playerwithattackrange )
        {
            if (isFront())
            {
                agent.isStopped = true;
                walkPointSet = false;
                Chasing();
            }
            
        }
        else
        {
            agent.isStopped = true;
            Attacking();
        }

      
    }

    void movingAround()
    {
        if (!walkPointSet)
        {
            agent.isStopped = false;
            float randomX = Random.Range(-walkpointRange, walkpointRange);
            float randomZ = Random.Range(-walkpointRange, walkpointRange);

            walkpoint=new Vector3(transform.position.x+randomX,transform.position.y, transform.position.z+randomZ);

            walkPointSet = true;

        }

        if (walkPointSet)
        {
           
            agent.SetDestination(walkpoint);
        }

        Vector3 dis = transform.position - walkpoint;

        if (dis.magnitude < 2f)
            walkPointSet = false;

    }

    void Chasing()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
    }

    void Attacking()
    {
        agent.isStopped = false;
        agent.SetDestination(transform.position);

        transform.LookAt(player);
       
        if(currtime<=Time.time)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, playermask))
            {
                Debug.Log("The enemy attack the player");
            }
            currtime = Time.time + 1 / timebtnattacks;
           
        }
       

    }

}
