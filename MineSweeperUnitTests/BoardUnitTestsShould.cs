using System.Collections.Generic;
using MineSweeper_v01;
using Xunit;

namespace MineSweeperUnitTests
{
    public class BoardUnitTestsShould
    {
        [Theory]
        [InlineData(2, 2)]
        [InlineData(4, 4)]
        [InlineData(10, 10)]
        [InlineData(5, 5)]
        public void GenerateABoardOfTheCorrectSize(int size, int expected)
        {
            //Arrange
            
            //Act
            var result = GridFactory.NewGameGrid(size);
            
            //Assert
            Assert.Equal(expected, result.Size);
        }

        [Theory]
        [InlineData(2, ". . \n. . \n")]
        [InlineData(3, ". . . \n. . . \n. . . \n")]
        [InlineData(4, ". . . . \n. . . . \n. . . . \n. . . . \n")]
        [InlineData(5, ". . . . . \n. . . . . \n. . . . . \n. . . . . \n. . . . . \n")]
        public void DisplayABoardWithTheCorrectDimensions(int size, string expected)
        {
            //Arrange
            var newDisplay = GridFactory.NewGridDisplay(size);
            
            //Act
            var result = newDisplay.GenerateGameDisplay(size);
            
            //Assert
            Assert.Equal(expected, result);
        }

        private class StubForMineLocationZeroZero : IMineGenerator
        {
            public List<Cell> MineLocations(int gridSize)
            {
                var output = new List<Cell> {new Cell(0,0)};
                return output;
            }
        }
        
        [Fact]
        public void UpdateTheStatusOfACellToRecordAMineAsTrue()
        {
            //Arrange
            var gridSize = 2;
            var newGameGrid = GridFactory.NewGameGrid(gridSize);
            newGameGrid.GenerateGrid(newGameGrid.Size);
            var updateCellMineStatus = new MineLogic();
            var mineStub = new StubForMineLocationZeroZero();

            //Act
            updateCellMineStatus.UpdateCellMineStatus(mineStub.MineLocations(newGameGrid.Size), newGameGrid);

            //Assert
            Assert.True(newGameGrid.GeneratedGameCell[0,0].IsAMine);
        }
        
        [Fact]
        public void UpdateTheStatusOfACellToRecordAMineAsFalse()
        {
            //Arrange
            var gridSize = 2;
            var newGame = GridFactory.NewGameGrid(gridSize);
            newGame.GenerateGrid(newGame.Size);
            var updateCellMineStatus = new MineLogic();
            var mineStub = new StubForMineLocationZeroZero();

            //Act
            updateCellMineStatus.UpdateCellMineStatus(mineStub.MineLocations(newGame.Size), newGame);

            //Assert
            Assert.False(newGame.GeneratedGameCell[1,1].IsAMine);
        }
    }
}