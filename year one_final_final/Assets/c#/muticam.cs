﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class muticam : MonoBehaviour {
    public List<Transform> targets;
    public Vector3 offset;
    private Vector3 velocity;
    public float smoothTime = .5f;
    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomlimiter = 50f;
    private Camera cam;
    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }
        Move();
        Zoom();

    }
    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetgreatstDistance() / zoomlimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }
    void Move() {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }
    float GetgreatstDistance()
    {
        var bounds = new Bounds(targets[0].position,Vector3.zero);
        for(int i = 0 ;i<targets.Count; i++)
        {
            if (targets[i] != null)
            {
                bounds.Encapsulate(targets[i].position);
            }
        }
        return bounds.size.x;
    }
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            
            if (targets[i] != null)
            {
                bounds.Encapsulate(targets[i].position);
            }
            if (targets[i] == null)
            {
                targets.Remove(targets[i]);
            }
        }
        return bounds.center;

    }
    
    


}
