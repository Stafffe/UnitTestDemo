using Moq;
using System;
using System.ComponentModel.DataAnnotations;
using UnitTestDemo.Business;
using UnitTestDemo.Interfaces;
using Xunit;

namespace UnitTestDemo.Tests.Business
{
    public class FruitBusinessTests
    {
        private readonly Mock<IDatabaseProvider> _databaseMock;
        private readonly FruitBusiness _sut;

        public FruitBusinessTests()
        {
            _databaseMock = new Mock<IDatabaseProvider>();

            _sut = new FruitBusiness(_databaseMock.Object);
        }

        [Fact]
        public void GetFriut_WithFruitNameWithLessThan5Letters_ThenGetTheNameFromDatabase()
        {
            //Arrenge
            var expected = "1234";
            var fruitId = 1;
            _databaseMock
                .Setup(mock => mock.GetFruitFromDatabase(fruitId))
                .Returns(expected);

            //Act
            var result = _sut.GetFruit(fruitId);

            //Assert
            _databaseMock
                .Verify(mock => mock.GetFruitFromDatabase(1), Times.Exactly(1));
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetFriut_With6Letters_ThenGetTheNameFromDatabaseReversed()
        {
            //Arrenge
            var expected = "egnarO";
            var fruitId = 1;
            _databaseMock
                .Setup(mock => mock.GetFruitFromDatabase(fruitId))
                .Returns("Orange");

            //Act
            var result = _sut.GetFruit(fruitId);

            //Assert
            _databaseMock
                .Verify(mock => mock.GetFruitFromDatabase(1), Times.Once);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetFriut_WithNegativeId_ShouldThrowValidationException()
        {
            //Arrenge
            var fruitId = -1;

            //Act/Assert
            Assert.Throws<ValidationException>(() => _sut.GetFruit(fruitId));
            _databaseMock
                .Verify(mock => mock.GetFruitFromDatabase(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void GetFriut_WithNullFruitFound_ShouldThrowValidationException()
        {
            //Arrenge
            var fruitId = 1000000000;
            _databaseMock
                .Setup(mock => mock.GetFruitFromDatabase(fruitId))
                .Returns((string)null);

            //Act

            Assert.Throws<ValidationException>(() => _sut.GetFruit(fruitId));
            _databaseMock
                .Verify(mock => mock.GetFruitFromDatabase(fruitId), Times.Exactly(1));
        }

        [Fact]
        public void GetFriut_WithFruitNameExactly5Letters_ThenGetTheNameFromDatabaseReversed()
        {
            //Arrenge
            var expected = "54321";
            var fruitId = 1;
            _databaseMock
                .Setup(mock => mock.GetFruitFromDatabase(fruitId))
                .Returns("12345");

            //Act
            var result = _sut.GetFruit(fruitId);

            //Assert
            _databaseMock
                .Verify(mock => mock.GetFruitFromDatabase(1), Times.Exactly(1));
            Assert.Equal(expected, result);
        }
    }
}
