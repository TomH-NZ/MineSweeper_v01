using System;
using System.Collections.Generic;
using MineSweeper.Factories;
using MineSweeper.Grid;
using MineSweeper.Interfaces;

namespace MineSweeper.Mines
{
    public class MineGenerator : IMineGenerator
    {
        public List<Cell> MineLocations(int gridSize)
        {
            var generatedMineList = new List<Cell>();
            var gameGrid = GridFactory.NewGameGrid(gridSize);

            for (var row = 0; row < gameGrid.Size; row++)
            {
                for (var column = 0; column < gameGrid.Size; column++)
                {
                    generatedMineList.Add(gameGrid.GeneratedGameCell[row, column]);
                    gameGrid.GeneratedGameCell[row, column].IsMine = true;
                }
            }

            var selectedMineLocations = new List<Cell>(); 

            for (var cell = 0; cell < gameGrid.Size; cell++)
            {
                var rnd = new Random();
                var randomMine = generatedMineList.Count;
                var mine = rnd.Next(randomMine);
                selectedMineLocations.Add(generatedMineList[mine]);
                generatedMineList.Remove(generatedMineList[mine]);
            }
            
            return selectedMineLocations;
        }
    }
}
