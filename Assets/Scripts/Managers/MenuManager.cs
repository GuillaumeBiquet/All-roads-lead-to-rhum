using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : Manager<MenuManager> {

	[Header("Panels")]
	[SerializeField] GameObject m_PanelMainMenu;
	[SerializeField] GameObject m_PanelInGameMenu;
	[SerializeField] GameObject m_PanelVictory;
	[SerializeField] GameObject m_PanelOption;
    [SerializeField] GameObject m_PanelLevel;
    [SerializeField] GameObject m_PanelCredits;
    //public InputField m_BackInput;
    List<GameObject> m_AllPanels;

	protected override IEnumerator InitCoroutine()
	{
		m_AllPanels = new List<GameObject>();
		m_AllPanels.Add(m_PanelMainMenu);
		m_AllPanels.Add(m_PanelInGameMenu);
		m_AllPanels.Add(m_PanelVictory);
		m_AllPanels.Add(m_PanelOption);
        m_AllPanels.Add(m_PanelLevel);
        m_AllPanels.Add(m_PanelCredits);
        yield break;
	}

	void OpenPanel(GameObject panel)
	{
		foreach (var item in m_AllPanels)
			if(item) item.SetActive(item == panel);
	}

	private void Update()
	{
		if(Input.GetButtonDown("Cancel"))
		{
			EscapeButtonHasBeenClicked();
		}        
	}

	public void EscapeButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new EscapeButtonClickedEvent());
	}
    public void RetryButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new RetryButtonClickedEvent());
    }
    public void PlayButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new PlayButtonClickedEvent());
	}
    public void LevelButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new LevelButtonClickedEvent());
    }

    public void ResumeButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new ResumeButtonClickedEvent());
	}

	public void MainMenuButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
	}
    public void BackVictoryButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new BackVictoryButtonClickedEvent());
    }
    public void MainMenuButtonReloadSceneHasBeenClicked()
    {
        EventManager.Instance.Raise(new MainMenuButtonReloadSceneClickedEvent());
    }
    public void QuitHasBeenClicked()
    {
        EventManager.Instance.Raise(new QuitButtonClickedEvent());
    }
    public void OptionButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new OptionButtonClickedEvent());
    }
    public void BackMenuButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new BackMenuButtonClickedEvent());
    }
    public void BackOptionButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new BackOptionButtonClickedEvent());
    }
    public void CameraInversionButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new CameraInversionButtonClickedEvent());
    }
    public void CreditButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new CreditsButtonClickedEvent());
    }

    //GameManager Events
    protected override void GameMenu(GameMenuEvent e)
	{
		OpenPanel(m_PanelMainMenu);
        GameObject FTPno = GameObject.Find("StartButton");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
    }

	protected override void GamePlay(GamePlayEvent e)
	{
		OpenPanel(null);
	}
    protected override void GameLevel(GameLevelEvent e)
    {
        OpenPanel(m_PanelLevel);
        GameObject FTPno = GameObject.Find("ButtonLevel1");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
    }

    protected override void GamePause(GamePauseEvent e)
	{
		OpenPanel(m_PanelInGameMenu);
        GameObject FTPno = GameObject.Find("ResumeButton");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
    }

	protected override void GameResume(GameResumeEvent e)
	{
		OpenPanel(null);
	}

	protected override void GameOption(GameOptionEvent e)
	{
		OpenPanel(m_PanelOption);
        GameObject FTPno = GameObject.Find("BackButton");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }

    }

	protected override void GameVictory(GameVictoryEvent e)
	{
		OpenPanel(m_PanelVictory);
        GameObject FTPno = GameObject.Find("MainMenuButton");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
    }

    protected override void GameCredits(GameCreditsEvent e)
    {
        OpenPanel(m_PanelCredits);
        GameObject FTPno = GameObject.Find("BackButton");
        if (FTPno)
        {
            EventSystem.current.SetSelectedGameObject(FTPno);
        }
    }
}
