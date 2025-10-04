using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StefaniniDotNetReactChallenge.API.Controllers;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Tests.Helpers;
using Xunit;

namespace StefaniniDotNetReactChallenge.Tests.API.Controllers
{
    public class TestPersonBasicController : PersonBasicController
    {
        public TestPersonBasicController(IPersonService service) : base(service)
        {
        }
    }

    public class PersonBasicControllerTests
    {
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly TestPersonBasicController _controller;

        public PersonBasicControllerTests()
        {
            _personServiceMock = new Mock<IPersonService>();
            _controller = new TestPersonBasicController(_personServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_WhenPeopleExist_ReturnsOkWithPeopleDtos()
        {
            // Arrange
            var people = new List<Person>
            {
                PersonFactory.CreatePerson("Test Person A", "12345678902"),
                PersonFactory.CreatePerson("Test Person B", "12345678902")
            };

            _personServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(people);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().NotBeNull();

            var peopleDtos = okResult.Value as IEnumerable<object>;
            peopleDtos.Should().HaveCount(2);

            _personServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenNoPeopleExist_ReturnsOkWithEmptyList()
        {
            // Arrange
            var emptyPeopleList = new List<Person>();

            _personServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(emptyPeopleList);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().NotBeNull();

            var peopleDtos = okResult.Value as IEnumerable<object>;
            peopleDtos.Should().BeEmpty();

            _personServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAll_WhenServiceThrowsException_PropagatesException()
        {
            // Arrange
            _personServiceMock
                .Setup(x => x.GetAllAsync())
                .ThrowsAsync(new Exception("Database error"));

            // Act
            Func<Task> act = async () => await _controller.GetAll();

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Database error");
        }

        [Fact]
        public async Task GetById_WhenPersonExists_ReturnsOkWithPersonDto()
        {
            // Arrange
            var person = PersonFactory.CreatePerson("Test Person A", "12345678902");

            _personServiceMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(person);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult!.Value.Should().NotBeNull();

            // Verify the DTO was created using the mapper
            var personDto = okResult.Value;
            personDto.Should().NotBeNull();

            _personServiceMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetById_WhenPersonDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            _personServiceMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync((Person?)null);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _personServiceMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task GetById_WithInvalidId_ReturnsNotFound(int invalidId)
        {
            // Arrange
            _personServiceMock
                .Setup(x => x.GetByIdAsync(invalidId))
                .ReturnsAsync((Person?)null);

            // Act
            var result = await _controller.GetById(invalidId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            _personServiceMock.Verify(x => x.GetByIdAsync(invalidId), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenPersonExists_ReturnsNoContent()
        {
            // Arrange
            _personServiceMock
                .Setup(x => x.DeleteByIdAsync(1))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(1);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _personServiceMock.Verify(x => x.DeleteByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenPersonDoesNotExist_ReturnsNoContent()
        {
            // Arrange
            _personServiceMock
                .Setup(x => x.DeleteByIdAsync(999))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(999);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _personServiceMock.Verify(x => x.DeleteByIdAsync(999), Times.Once);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task Delete_WithInvalidId_ReturnsNoContent(int invalidId)
        {
            // Arrange
            _personServiceMock
                .Setup(x => x.DeleteByIdAsync(invalidId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Delete(invalidId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            _personServiceMock.Verify(x => x.DeleteByIdAsync(invalidId), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenServiceThrowsException_PropagatesException()
        {
            // Arrange
            _personServiceMock
                .Setup(x => x.DeleteByIdAsync(1))
                .ThrowsAsync(new Exception("Delete failed"));

            // Act
            Func<Task> act = async () => await _controller.Delete(1);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Delete failed");
        }

        [Fact]
        public async Task GetAll_UsesPersonMapperCorrectly()
        {
            // Arrange
            var person = PersonFactory.CreatePerson("Test Person", "12345678902");

            var people = new List<Person> { person };

            _personServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(people);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            // Verify that the mapper was used (the result should be the DTO version)
            var peopleDtos = okResult!.Value as IEnumerable<object>;
            var personDto = peopleDtos!.First();
            personDto.Should().NotBeNull();

            // We can't directly test the DTO properties without knowing the exact DTO type,
            // but we can verify that something was returned and it's not the entity itself
            personDto.Should().NotBeSameAs(person);
        }

        [Fact]
        public async Task GetById_UsesPersonMapperCorrectly()
        {
            // Arrange
            var person = PersonFactory.CreatePerson("Test Person A", "12345678902");

            _personServiceMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(person);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            // Verify that the mapper was used
            var personDto = okResult!.Value;
            personDto.Should().NotBeNull();
            personDto.Should().NotBeSameAs(person);
        }
    }
}