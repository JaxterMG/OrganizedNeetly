using UnityEditor;
using UnityEngine;
using Core.Controllers;
using Core.StateMachine.Game;
using Core.StateMachine.Menu;
using Core.StateMachine.Loading;
using Core.StateMachine;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameController gameController = (GameController)target;
        
        if (GUILayout.Button("Set to Game State"))
        {
            State state = new GameState(gameController);
            gameController.ChangeState(state);
        }

        if (GUILayout.Button("Set to Menu State"))
        {
            State state = new MainMenuState(gameController);
            gameController.ChangeState(state);
        }

        if (GUILayout.Button("Set to Loading State"))
        {
            State state = new LoadingState(gameController);
            gameController.ChangeState(state);
        }
    }
}
