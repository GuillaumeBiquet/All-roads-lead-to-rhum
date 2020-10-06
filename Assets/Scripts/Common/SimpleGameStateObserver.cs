using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public abstract class SimpleGameStateObserver : MonoBehaviour,IEventHandler {

	public virtual void SubscribeEvents()
	{
		EventManager.Instance.AddListener<GameMenuEvent>(GameMenu);
		EventManager.Instance.AddListener<GamePlayEvent>(GamePlay);
		EventManager.Instance.AddListener<GamePauseEvent>(GamePause);
        EventManager.Instance.AddListener<GameLevelEvent>(GameLevel);
        EventManager.Instance.AddListener<GameResumeEvent>(GameResume);
		EventManager.Instance.AddListener<GameOptionEvent>(GameOption);
		EventManager.Instance.AddListener<GameVictoryEvent>(GameVictory);
		EventManager.Instance.AddListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
        EventManager.Instance.AddListener<GameCreditsEvent>(GameCredits);
    }

	public virtual void UnsubscribeEvents()
	{
		EventManager.Instance.RemoveListener<GameMenuEvent>(GameMenu);
		EventManager.Instance.RemoveListener<GamePlayEvent>(GamePlay);
        EventManager.Instance.RemoveListener<GameLevelEvent>(GameLevel);
        EventManager.Instance.RemoveListener<GamePauseEvent>(GamePause);
		EventManager.Instance.RemoveListener<GameResumeEvent>(GameResume);
		EventManager.Instance.RemoveListener<GameOptionEvent>(GameOption);
		EventManager.Instance.RemoveListener<GameVictoryEvent>(GameVictory);
		EventManager.Instance.RemoveListener<GameStatisticsChangedEvent>(GameStatisticsChanged);
        EventManager.Instance.RemoveListener<GameCreditsEvent>(GameCredits);

    }

    // Use this for initialization
    protected virtual IEnumerator Start () {
		SubscribeEvents();
		yield break;
	}

	protected virtual void OnDestroy()
	{
		UnsubscribeEvents();
	}

	protected virtual void GameMenu(GameMenuEvent e)
	{
	}

	protected virtual void GamePlay(GamePlayEvent e)
	{ 
	}

	protected virtual void GamePause(GamePauseEvent e)
	{
	}

	protected virtual void GameResume(GameResumeEvent e)
	{
	}

	protected virtual void GameOption(GameOptionEvent e)
	{
	}

	protected virtual void GameVictory(GameVictoryEvent e)
	{
	}
    protected virtual void GameLevel(GameLevelEvent e)
    {
    }
    protected virtual void GameCredits(GameCreditsEvent e)
    {
    }

    protected virtual void GameStatisticsChanged(GameStatisticsChangedEvent e)
	{
	}
}
