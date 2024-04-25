using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem gunParticle;
    public Transform firePosition;
    private int damage = 50;

    // ÃÑ¾Ë ±ËÀû
    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }

    private void Start()
    {

    }

    private void Update()
    {
        Shot();
    }

    private void Shot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(firePosition.position, firePosition.forward);

            if (Physics.Raycast(ray , out RaycastHit hit, 100f))
            {
                StartCoroutine(ShotEffect(hit.point));

                if (hit.collider.CompareTag("Enemy"))
                {
                    var enemy = hit.collider.gameObject.GetComponent<Enemy>();

                    enemy.OnDamege(damage, hit.point);
                }
            }

        }
    }

    IEnumerator ShotEffect(Vector3 hitPosition)
    {
        gunParticle.Play();
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, firePosition.position);
        lineRenderer.SetPosition(1, hitPosition);

        yield return new WaitForSeconds(0.1f);

        lineRenderer.enabled = false;
    }
}
