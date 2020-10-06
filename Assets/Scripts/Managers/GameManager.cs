using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using UnityEngine.SceneManagement;

public enum GameState { gameMenu,gamePlay,gamePause,gameOption,gameVictory,gameLevel, gameCredits}

public class GameManager : Manager<GameManager> {

	//Game State
	private GameState m_GameState;
	public bool IsPlaying { get { return m_GameState == GameState.gamePlay; } }
    public bool IsPlayingInOption { get { return m_GameState == GameState.gameOption; } }
    //
    private int CameraDirection = 1;
	// TIME SCALE
	private float m_TimeScale;
	public float TimeScale { get { return m_TimeScale; } }
	void SetTimeScale(float newTimeScale)
	{
		m_TimeScale = newTimeScale;
		Time.timeScale = m_TimeScale;
	}

	//SCORE
	private int m_Score;
	public int Score {
		get { return m_Score; }
		set
		{
			m_Score = value;
			BestScore = Mathf.Max(BestScore, value);
		}
	}

	public int BestScore
	{
		get
		{
			return PlayerPrefs.GetInt("BEST_SCORE", 0);
		}
		set
		{
			PlayerPrefs.SetInt("BEST_SCORE", value);
		}
	}

	void IncrementScore(int increment)
	{
		SetScore(m_Score + increment);
	}

	void SetScore(int score)
	{
		Score = score;
		EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eBestScore = BestScore,eScore =m_Score});
	}

	public override void SubscribeEvents()
	{
		base.SubscribeEvents();
		EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
		EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.AddListener<BackMenuButtonClickedEvent>(BackMenuButtonClicked);
        EventManager.Instance.AddListener<BackOptionButtonClickedEvent>(BackOptionButtonClicked);
        EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitButtonClicked);
        EventManager.Instance.AddListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
		EventManager.Instance.AddListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.AddListener<OptionButtonClickedEvent>(OptionButtonClicked);
        EventManager.Instance.AddListener<LevelButtonClickedEvent>(LevelButtonClicked);
        EventManager.Instance.AddListener<RetryButtonClickedEvent>(RetryButtonClicked);
        EventManager.Instance.AddListener<CameraInversionButtonClickedEvent>(CameraInversionButtonClicked);
        EventManager.Instance.AddListener<VictoryObtainedEvent>(VictoryObtained);
        EventManager.Instance.AddListener<BackVictoryButtonClickedEvent>(BackVictoryButtonClicked);
        EventManager.Instance.AddListener<CreditsButtonClickedEvent>(CreditsButtonClicked); 
    }

	public override void UnsubscribeEvents()
	{
		base.UnsubscribeEvents();

		EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
		EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
        EventManager.Instance.RemoveListener<VictoryObtainedEvent>(VictoryObtained);
        EventManager.Instance.RemoveListener<RetryButtonClickedEvent>(RetryButtonClicked);
        EventManager.Instance.RemoveListener<BackMenuButtonClickedEvent>(BackMenuButtonClicked);
        EventManager.Instance.RemoveListener<BackOptionButtonClickedEvent>(BackOptionButtonClicked);
        EventManager.Instance.RemoveListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
        EventManager.Instance.RemoveListener<LevelButtonClickedEvent>(LevelButtonClicked);
        EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitButtonClicked);
        EventManager.Instance.RemoveListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
        EventManager.Instance.RemoveListener<OptionButtonClickedEvent>(OptionButtonClicked);
        EventManager.Instance.RemoveListener<CameraInversionButtonClickedEvent>(CameraInversionButtonClicked);
        EventManager.Instance.RemoveListener<BackVictoryButtonClickedEvent>(BackVictoryButtonClicked);
        EventManager.Instance.RemoveListener<CreditsButtonClickedEvent>(CreditsButtonClicked);
    }

	protected override IEnumerator InitCoroutine()
	{
        while(!MenuManager.Instance.IsReady) yield return null;
		Menu();
		yield break;
	}

	private void InitNewGame(int level)
	{
        
        SetScore(0);
	}

	//Buttons Events
	private void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
	{
        //if (IsPlaying)
        //{
        //    SceneManager.UnloadScene("lvl1");
        //    Menu();
        //}
        // else   
        Menu();
	}

	private void PlayButtonClicked(PlayButtonClickedEvent e)
	{
		Play();
	}
    private void RetryButtonClicked(RetryButtonClickedEvent e)
    {
        Retry();
    }
    private void BackVictoryButtonClicked(BackVictoryButtonClickedEvent e)
    {
        BackVictory();
    }
    private void BackVictory()
    {
        EventManager.Instance.Raise(new GameVictoryEvent());
    }
    private void MainMenuButtonReloadSceneClicked(MainMenuButtonReloadSceneClickedEvent e)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Menu();
    }

	private void ResumeButtonClicked(ResumeButtonClickedEvent e)
	{
		Resume();
	}
    private void LevelButtonClicked(LevelButtonClickedEvent e)
    {
        LevelSelection();
    }

    private void EscapeButtonClicked(EscapeButtonClickedEvent e)
	{
		if(IsPlaying)
			Pause();
	}
    private void OptionButtonClicked(OptionButtonClickedEvent e)
    {
        Option();
    }
    private void CameraInversionButtonClicked(CameraInversionButtonClickedEvent e)
    {
        CameraInversion();
    }
    private void QuitButtonClicked(QuitButtonClickedEvent e)
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    private void BackMenuButtonClicked(BackMenuButtonClickedEvent e)
    {
        BackMenu();
    }
    private void BackOptionButtonClicked(BackOptionButtonClickedEvent e)
    {
        BackOption();
    }
    private void CreditsButtonClicked(CreditsButtonClickedEvent e)
    {
        Credits();
    }
    private void VictoryObtained(VictoryObtainedEvent e)
    {
        Victory();
    }

    //Game State events
    private void Menu()
	{
		SetTimeScale(0);
		m_GameState = GameState.gameMenu;
		EventManager.Instance.Raise(new GameMenuEvent());
	}

	private void Play()
    {
        InitNewGame(1);
        SetTimeScale(1);
        ScoreScript.scoreValue = 0;
        ScoreScript.barrelValue = 0;
        m_GameState = GameState.gamePlay;
		EventManager.Instance.Raise(new GamePlayEvent());
	}

	private void Pause()
	{
		SetTimeScale(0);
		m_GameState = GameState.gamePause;
		EventManager.Instance.Raise(new GamePauseEvent());
	}
    private void LevelSelection()
    {
        m_GameState = GameState.gameLevel;
        EventManager.Instance.Raise(new GameLevelEvent());
    }

    private void Resume()
	{
		SetTimeScale(1);
		m_GameState = GameState.gamePlay;
		EventManager.Instance.Raise(new GamePlayEvent());
	}
    private void Retry()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ScoreScript.scoreValue = 0;
        ScoreScript.barrelValue = 0;
    }

    private void Option()
	{
		SetTimeScale(0);
		
		EventManager.Instance.Raise(new GameOptionEvent());
	}

    private void CameraInversion()
    {
        CameraDirection=CameraDirection*(-1);

    }

    private void BackOption()
    {
        
        EventManager.Instance.Raise(new GamePauseEvent());

    }
    private void BackMenu()
    {

        EventManager.Instance.Raise(new GameMenuEvent());

    }

    private void Victory()
	{
		SetTimeScale(0);
		m_GameState = GameState.gameVictory;
		EventManager.Instance.Raise(new GameVictoryEvent());
	}
    private void Credits()
    {
        m_GameState = GameState.gameCredits;
        EventManager.Instance.Raise(new GameCreditsEvent()); 
    }

}
