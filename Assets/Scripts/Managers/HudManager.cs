using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HudManager : Manager<HudManager> {

	[Header("Texts")]
	
	[SerializeField] private Text m_TxtScore;
    [SerializeField] private Text m_TxtLevel;

    protected override IEnumerator InitCoroutine()
	{
		yield break;
	}

	protected override void GameStatisticsChanged(GameStatisticsChangedEvent e)
	{
		
		m_TxtScore.text = e.eScore.ToString();
        m_TxtLevel.text = e.eNLevel.ToString();
    }
}
