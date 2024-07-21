using System.Collections.Generic;
using UnityEngine;

namespace Core.Grid.Figures
{
    public class ComplexFiguresGenerator : MonoBehaviour
    {
        [SerializeField] string _name;
        public List<Vector2> Shape;
        public GameObject CellPrefab;
        public float GapSize = 0.05f;
        public float CellSize = 0.35f;

        public void GenerateFigure(bool[,] shapeGrid, int gridWidth = 5, int gridHeight = 5)
        {
            GameObject parent = new GameObject(_name);
            for (int y = gridHeight - 1; y >= 0; y--)
            {
                for (int x = 0; x < gridWidth; x++)
                {
                    if (shapeGrid[x, y])
                    {
                        GameObject cell = Instantiate(CellPrefab);
                        cell.transform.SetParent(parent.transform);
                        float posX = (x - gridWidth / 2) * (CellSize + GapSize);
                        float posY = (y - gridHeight / 2) * (CellSize + GapSize);
                        cell.transform.localPosition = new Vector3(posX, posY, 0);
                        cell.transform.localScale = new Vector3(CellSize, CellSize, CellSize);
                    }
                }
            }

            parent.AddComponent<FigureDragHandler>().SetShape(Shape);
        }
    }
}
