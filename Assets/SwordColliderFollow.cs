using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordColliderFollow : MonoBehaviour
{

    #region Serialized Field

    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private MeshCollider meshCollider;

    #endregion

    #region Property



    #endregion

    #region Private Field
    
    

    #endregion

    #region MonoBehavior Callback

    private void Start()
    {
        Mesh colliderMesh = new Mesh();
        meshRenderer.BakeMesh(colliderMesh);
        meshCollider.sharedMesh = null;
        meshCollider.sharedMesh = colliderMesh;
    }

    // private void Update()
    // {
    //     
    // }

    #endregion

    #region Public Methods



    #endregion

    #region Private Methods



    #endregion


}
