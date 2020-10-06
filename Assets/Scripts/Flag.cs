using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class Flag : SimpleGameStateObserver {

    [SerializeField] private GameObject m_VictoryObject;
    
    private bool m_VictoryOK;

    public bool VictoryOK { get { return m_VictoryOK; } }
    protected override IEnumerator Start()
    {
        m_VictoryObject.SetActive(false);
        gameObject.SetActive(true);
        yield return StartCoroutine(base.Start());
    }
    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        EventManager.Instance.AddListener<AllBarrelEvent>(AllBarrel);
    }

    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        EventManager.Instance.RemoveListener<AllBarrelEvent>(AllBarrel);
    }

    private void AllBarrel(AllBarrelEvent e)
    {
        m_VictoryObject.SetActive(true);
        gameObject.SetActive(false);
        m_VictoryObject.GetComponent<Flag>().m_VictoryOK = true;
    }
}
