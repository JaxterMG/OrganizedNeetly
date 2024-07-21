using Core.Grid.Figures;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Core.Editor.Grid
{
    [CustomEditor(typeof(ComplexFiguresGenerator))]
    public class ComplexFiguresGeneratorEditor : UnityEditor.Editor
    {
        ComplexFiguresGenerator element;
        bool[,] shapeGrid;
        int gridWidth = 5;
        int gridHeight = 5;

        void OnEnable()
        {
            element = (ComplexFiguresGenerator) target;
            shapeGrid = new bool[gridWidth, gridHeight];
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            // Draw grid.
            for (int y = gridHeight - 1; y >= 0; y--)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < gridWidth; x++)
                {
                    // Get current state and draw button.
                    bool currentState = shapeGrid[x, y];
                    string buttonText = currentState ? "X" : "";
                    if (GUILayout.Button(buttonText, GUILayout.Width(20), GUILayout.Height(20)))
                    {
                        // Toggle state when button is clicked.
                        shapeGrid[x, y] = !currentState;
                    }
                }

                EditorGUILayout.EndHorizontal();
            }

            // Update Shape list when Apply button is clicked.
            if (GUILayout.Button("Apply Shape"))
            {
                element.Shape = new List<Vector2>();
                for (int y = gridHeight - 1; y >= 0; y--)
                {
                    for (int x = 0; x < gridWidth; x++)
                    {
                        if (shapeGrid[x, y])
                        {
                            // Add Vector2 to Shape list if the cell is part of the shape.
                            element.Shape.Add(new Vector2(x - gridWidth / 2,
                                y - gridHeight / 2)); // Subtracting gridWidth/2 and gridHeight/2 to center the shape.
                        }
                    }
                }
            }

            if (GUILayout.Button("Generate Cells"))
            {
                element.GenerateFigure(shapeGrid, gridWidth, gridHeight);
            }
        }
    }
}