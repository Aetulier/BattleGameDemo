﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RoleData {

    private const string PREFIX_PREFAB = "Prefabs/";
    public  RoleType RoleType { get; private set; }
    public GameObject RolePreFab { get; private set; }
    public GameObject ArrowPrefab { get; private set; }
    public Vector3 SpawnPositon { get; private set; }

    public GameObject ExplosionEffect { get; private set; }
   
    public RoleData(RoleType roleType,string rolePath,string arrowPath,string explosionPath,Transform spawnPosition)
    {
        this.RoleType = roleType;
        this.SpawnPositon = spawnPosition.position;
        this.RolePreFab = Resources.Load(PREFIX_PREFAB + rolePath) as GameObject;
        this.ArrowPrefab = Resources.Load(PREFIX_PREFAB + arrowPath) as GameObject;
        this.ExplosionEffect= Resources.Load(PREFIX_PREFAB + explosionPath) as GameObject;
        ArrowPrefab.GetComponent<Arrow>().explosionEffect = ExplosionEffect;
    }

}
