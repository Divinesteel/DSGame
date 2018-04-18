using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshJumper : MonoBehaviour
{
    public float height;
    public float duration;
    public List<GameObject> MeshLinks = new List<GameObject>();
    private string OffMeshName;

    public IEnumerator Start()
    {

        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;


        //agent.currentOffMeshLinkData.offMeshLink.gameObject.tag

        while (true)
        {
            if (agent.isOnOffMeshLink)
            {
                yield return StartCoroutine(Parabola(agent, height, duration));            

                agent.CompleteOffMeshLink();
                
            }
            yield return null;
        }
    }

    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        OffMeshLinkData data = agent.currentOffMeshLinkData;
        Vector3 startPos = agent.transform.position;
        Vector3 endPos = data.endPos;
     
        Vector3 relativePos = endPos - agent.transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos);

        float time = 0f;
        float timerot = 0f;

        while (time < 1f)
        {
            float yOffset = height * (time - time * time);
            agent.transform.position = Vector3.Lerp(startPos, endPos, time) + yOffset * Vector3.up;

            agent.transform.rotation = Quaternion.Lerp(agent.transform.rotation, rotation, timerot);

            timerot += Time.deltaTime / (float)0.5;

            time += Time.deltaTime / duration;
            yield return null;
        }
    }

}