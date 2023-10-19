﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fow_Script : MonoBehaviour {


    public static List<FowUnit> fowUnits = new List<FowUnit>(); 

    public Vector3 fowPlaneScale;
    public Vector3 planeOffset;
    public float planeRotation; 

    public Color color_fog = Color.black;
    public GameObject prefab_fowPlane;

    private GameObject fogPlane; //시야 플랜

    private Camera fowCamera;
    private Camera mainCamera;


    private Mesh fogPlaneMesh;
    private Vector3[] fogPlaneVertices;
    private Color[] fogPlaneColors;


    public float updateFrequency; 
    private float updateTimer;

    void Start() {

        mainCamera = transform.parent.GetComponent<Camera>();
        fowCamera = GetComponent<Camera>();
        fowCamera.depth = mainCamera.depth+1; 
        fowCamera.farClipPlane = mainCamera.farClipPlane;
        fowCamera.nearClipPlane = mainCamera.nearClipPlane;

        fogPlane = GameObject.Instantiate(prefab_fowPlane); // 시야생성
        Skill.instance.vision = fogPlane;
        if (fogPlane.layer==0) {
            Debug.LogError("Error: Fog plane is missing the FOW layer!");
        }
        fogPlaneMesh = fogPlane.GetComponent<MeshFilter>().mesh;
        fogPlane.GetComponent<Renderer>().material.SetColor("_TintColor",color_fog);
        fogPlaneVertices = fogPlaneMesh.vertices;
        fogPlaneColors = new Color[fogPlaneVertices.Length];
        for (int i = 0; i < fogPlaneColors.Length; i++) {
            fogPlaneColors[i] = color_fog;
        }
    }

    void Update() {

        fogPlane.transform.position = new Vector3(mainCamera.transform.position.x, 0, mainCamera.transform.position.z) + planeOffset; // make the fog plane follow the camera with an offset
        fogPlane.transform.localScale = fowPlaneScale;
        fogPlane.transform.localRotation = Quaternion.Euler(270, 0, planeRotation);
        fowCamera.fieldOfView = mainCamera.fieldOfView;

        updateTimer -= 1 * Time.deltaTime;
        if (updateTimer <= 0) {
            updateTimer = updateFrequency;


            for (int i = 0; i < fogPlaneVertices.Length; i++) {
                fogPlaneColors[i].a = 1; // set all vertices to opaque
            }
            foreach (FowUnit unit in fowUnits) {
                for (int i = 0; i < fogPlaneVertices.Length; i++) {
                    Vector3 v = fogPlane.transform.TransformPoint(fogPlaneVertices[i]);
                    float dist = Vector3.SqrMagnitude(v - new Vector3(unit.transform.position.x, fogPlane.transform.position.y, unit.transform.position.z));
                    float alpha = Mathf.Min(fogPlaneColors[i].a, (dist - unit.edgeSharpness * 50) / (unit.radius * unit.radius));
                    fogPlaneColors[i].a = alpha; // set transparency based on distance
                }
            }
            fogPlaneMesh.colors = fogPlaneColors;
        }
    }

}