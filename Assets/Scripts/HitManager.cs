using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HitManager : MonoBehaviour
{
    [System.Serializable]
    public struct HitInfo
    {
        public Vector2 groundForce;
        public float verticalForce;
        public int damage;
        public float stuntTime;
    }
    private static HitManager _instance;
    public static HitManager Instance { get {return _instance; } }
    public float hitMargin = 0.3f;
    #region Singleton
    private void Awake()
    {
        if (_instance==null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion
    public bool CheckHit(Transform owner, HitReceiver hitReceiver,HitInfo hitInfo)
    {
        if (MathF.Abs(owner.transform.position.y-hitReceiver.transform.position.y)<=hitMargin)
        {
            hitReceiver.Hit(hitInfo);
            return true;
        }
        return false;
    }
}
