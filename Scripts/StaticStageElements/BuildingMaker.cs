using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMaker : MonoBehaviour
{
    [SerializeField] Material sideTexture;
    [SerializeField] Material frontTexture;
    [SerializeField] Material upTexture;
    [SerializeField] float tillingFrequence = 2;
    private Renderer planeTopRenderer, planeSide1Renderer, planeSide2Renderer, planeBackRenderer, planeFrontRenderer;
    //private float minXY, minYZ, minXZ;

    private void Start()
    {
        planeTopRenderer = transform.GetChild(0).GetComponent<Renderer>();
        planeSide1Renderer = transform.GetChild(1).GetComponent<Renderer>();
        planeSide2Renderer = transform.GetChild(2).GetComponent<Renderer>();
        planeBackRenderer = transform.GetChild(3).GetComponent<Renderer>();
        planeFrontRenderer = transform.GetChild(4).GetComponent<Renderer>();

        planeTopRenderer.material = upTexture;
        planeSide1Renderer.material = sideTexture;
        planeSide2Renderer.material = sideTexture;
        planeBackRenderer.material = frontTexture;
        planeFrontRenderer.material = frontTexture;

        checkProportions();
    }

    private void checkProportions()
    {
        bool isProportional = transform.localScale.x == transform.localScale.y && transform.localScale.x == transform.localScale.z;
        //float min = -727;
        //get the different one and do the cases
        if (!isProportional)
        {
            //if (minNumScale() != 0) min = minNumScale();
            //numberAssignor();
            planeTopRenderer.material.mainTextureScale = new Vector2(transform.localScale.x/tillingFrequence, transform.localScale.z/tillingFrequence); //top changes with X and Z proportions
            
            planeSide1Renderer.material.mainTextureScale = new Vector2(transform.localScale.x / tillingFrequence, transform.localScale.y / tillingFrequence); //sides only X and Y
            planeSide2Renderer.material.mainTextureScale = planeSide1Renderer.material.mainTextureScale;

            planeFrontRenderer.material.mainTextureScale = new Vector2(transform.localScale.z / tillingFrequence, transform.localScale.y / tillingFrequence); //front only Y and Z
            planeBackRenderer.material.mainTextureScale = planeFrontRenderer.material.mainTextureScale;
        }
    }
    //private void numberAssignor() {
    //    float x = transform.localScale.x;
    //    float y = transform.localScale.y;
    //    float z = transform.localScale.z;

    //    if (x > z)
    //    {
    //        minXZ = x;
    //    }
    //    else minXZ = z;
    //    if (y > z) {
    //        minYZ = y;
    //    }
    //    else minYZ = z;
    //    if (x > y) {
    //        minXY = x;
    //    }
    //    else minXY = y;

    //}
    //private float minNumScale() {
    //    float x = transform.localScale.x;
    //    float y = transform.localScale.y;
    //    float z = transform.localScale.z;

    //    if (x <= y && x <= z) return x;
    //    if (y <= x && y <= z) return y;
    //    if (z <= x && z <= y) return z;
    //    else return 0;
    //}
}
