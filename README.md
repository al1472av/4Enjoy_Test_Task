# Task

# Description
# Used plugins

 1. Addressables
 2. Odin Inspector
 3. Text Mesh Pro
 4. DOTween
 5. Json Newtonsoft
 6. UniTask

# Hierarchy of folders in the project

 1. External Assets - Contains all external assets\plugins that were imported
 2. Life Game - Contains all game files, like: Script, Prefabs, Sprites, Animations etc.

# Scripts

 1. Game Data - Contains all static data of game, in this case it a scriptable objects
	 1. Config - Contains main settings of game: Max\Min life count, Cooldown 
 2.  Services - Main services of game
	 1. Addressables service - provides a logic of loading and unloading assets
	 2. UIFactory service - provides a logic of creating UI objects;
	 3. GameData service - provides static data of game
	 4. Loading service - provides a logic of loading any object that implements ILoadingOperation
	 5. PlayerData service - porvides player data, load and save
	 6. SceneLoader service - provides logic of loading scenes
	 7. StateMachine service - provides a logic of changing game state
	 8. Time service - provides logic of Timer and operations with "Seasons"
	 9. UI service - allows us to open close UI object (Windows, Popups)
	 10. UnityEvent service - allows us to use unity events from not MonoBehaviour classes 
 3. UI - All UI logic is here
	 1. Popups
		 2.  Daily Reward popup
		 3.  Lives Popup
	 2. Windows
		 1. MainWindow 

# How to run?

Just open Main scene "Assets/LifeGame/Scenes/Main.unity" and run. Game will automatically spawn all prefabs that it needs and will load the scene that it needs.

# A bit about architecture

Game is build with using of ServiceLocator + State Machine. ServiceLocator is a some kind of DI. It allows us to inject dependencies that we need into classes where we need by just asking services from ServiceProvider. All services must be used as properties, by contract and be provided by ServiceProvider, because services might be changed at runtime (not used in this projects).

# Entry Point
Game starts in script named as "Entry point", it validates some data, binds services and run state machine to start state - "AppStartupState". 
	"AppStartupState" should load and validate necessary data, because this task is simple there is no need for this procedures, so we just load "LoadMainLevelState".
	"LoadMainLevelState" loads main level, creates UIRoot and necessary windows, popups.

# About
Game has save\load system. Game has live refill system while its closed. At the start, based on time difference between game sessions, lives will refill. This also works with unfocus* from editor. 

Daily reward will be given 1 time, if you want to test it again, you can delete save file by "Tools/Player Data/Delete save". I have clamped value of given reward because with the system that was mentioned in task the values goes to infinity.

Unfocus* - focus on another application, not Unity. Its not about pause button in editor.
