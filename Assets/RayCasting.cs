using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayCasting : MonoBehaviour
{
    public int reflections;
    public float maxLenght;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;

    Touch touch;
    HitManager hitManager;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        hitManager = GameObject.Find("HitManager").GetComponent<HitManager>();
    }

    private void Update()
    {
        DrawLineThenShoot();
    }
    private void DrawLineThenShoot()
    {
        ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remaingLenght = maxLenght;

        for (int i = 0; i < reflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, remaingLenght))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remaingLenght -= Vector3.Distance(ray.origin, hit.point);
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
                if (hit.collider.tag != "Mirror")
                    break;
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remaingLenght);
            }
        }
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended)
        {
            if (hit.collider.tag == "Enemy")
            {
                GameObject enemy = hit.collider.gameObject;
                enemy.GetComponent<Animator>().SetBool("Dead", true);
                hitManager.addEnemyHit(1);
                
            }
        }
    }
}
