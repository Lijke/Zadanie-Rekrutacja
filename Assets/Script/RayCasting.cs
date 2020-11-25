﻿using System.Collections;
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
    public float timeTouchBegan;
    public float tapTime;

    public AudioSource audioSource;
    public AudioClip clip;
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        hitManager = GameObject.Find("HitManager").GetComponent<HitManager>();
    }

    private void Update()
    {
        DrawLineThenShoot();
    }
    private void Shoot(RaycastHit hit)
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                timeTouchBegan = Time.time;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                tapTime = Time.time - timeTouchBegan;
                Debug.Log(tapTime);
            }
            if(tapTime<0.2f && touch.phase==TouchPhase.Ended)
            {
                audioSource.PlayOneShot(clip,1);
                if (hit.collider.tag == "Enemy") 
                {
                    GameObject enemy = hit.collider.gameObject; 
                    enemy.GetComponent<Animator>().SetBool("Dead", true); 
                    hitManager.addEnemyHit(1); 
                    hitManager.returnScore();
                    StartCoroutine(DeleteObject(enemy));
                } 
                else if (hit.collider.tag == "Player") 
                { 
                    
                }
            }
        }
        
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
        Shoot(hit);
    }
    private IEnumerator DeleteObject(GameObject hit)
    {
        yield return new WaitForSeconds(1);
        Destroy(hit);
    }
}