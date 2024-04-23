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

    // 1. ���콺�� ��ũ�� ��ǥ Input.mousePosition  ��ũ����ǥ
    // 2. ���� (ī�޶� �ٶ󺸰��ִ� ���� , ���콺�� ��ũ�� ��ǥ )
    // 3. ���� ����Ʈ 
    // 4. agent < ������ǥ

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
