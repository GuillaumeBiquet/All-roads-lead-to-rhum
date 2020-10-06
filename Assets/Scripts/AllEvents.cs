using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;
using UnityEngine.SceneManagement;
#region GameManager Events
public class GameMenuEvent : SDD.Events.Event
{
}
public class GamePlayEvent : SDD.Events.Event
{
}
public class GamePauseEvent : SDD.Events.Event
{
}
public class GameLevelEvent : SDD.Events.Event
{
}
public class GameResumeEvent : SDD.Events.Event
{
}
public class GameOptionEvent : SDD.Events.Event
{
}
public class GameVictoryEvent : SDD.Events.Event
{
}

public class GameCreditsEvent : SDD.Events.Event
{
}

public class GameStatisticsChangedEvent : SDD.Events.Event
{
	public int eBestScore { get; set; }
	public int eScore { get; set; }
    public int eNLevel =SceneManager.GetActiveScene().buildIndex;
}
#endregion

#region MenuManager Events
public class EscapeButtonClickedEvent : SDD.Events.Event
{
}
public class PlayButtonClickedEvent : SDD.Events.Event
{
}
public class ResumeButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonClickedEvent : SDD.Events.Event
{
}
public class LevelButtonClickedEvent : SDD.Events.Event
{
}
public class MainMenuButtonReloadSceneClickedEvent : SDD.Events.Event
{
}
public class OptionButtonClickedEvent : SDD.Events.Event
{
}
public class CameraInversionButtonClickedEvent : SDD.Events.Event
{
}
public class QuitButtonClickedEvent : SDD.Events.Event
{
}
public class BackMenuButtonClickedEvent : SDD.Events.Event
{
}
public class BackOptionButtonClickedEvent : SDD.Events.Event
{
}
public class RetryButtonClickedEvent : SDD.Events.Event
{
}
public class VictoryObtainedEvent : SDD.Events.Event
{
}
public class AllBarrelEvent : SDD.Events.Event
{
}
public class BackVictoryButtonClickedEvent: SDD.Events.Event
{
}
public class CreditsButtonClickedEvent : SDD.Events.Event
{
}

#endregion