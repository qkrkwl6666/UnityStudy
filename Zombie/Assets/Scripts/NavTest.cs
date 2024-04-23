using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    private NavMeshAgent agent;
    public LayerMask layers;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        
    }

    // 1. 마우스의 스크린 좌표 Input.mousePosition  스크린좌표
    // 2. 레이 (카메라가 바라보고있는 방향 , 마우스의 스크린 좌표 )
    // 3. 레이 래스트 
    // 4. agent < 월드좌표

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, layers))
            {
                agent.SetDestination(hitInfo.point);
            }
        }

    }
}
