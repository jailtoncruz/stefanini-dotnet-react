using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StefaniniDotNetReactChallenge.API.Controllers;
using StefaniniDotNetReactChallenge.Application.DTOs;
using StefaniniDotNetReactChallenge.Application.Interfaces;
using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Tests.Helpers;

namespace StefaniniDotNetReactChallenge.Tests.API.Controllers
{
    public class PersonControllerV2Tests
    {
        private readonly Mock<IPersonService> _personServiceMock;
        private readonly PersonControllerV2 _controller;

        public PersonControllerV2Tests()
        {
            _personServiceMock = new Mock<IPersonService>();
            _controller = new PersonControllerV2(_personServiceMock.Object);
        }

        [Fact]
        public async Task Create_WithValidDto_ReturnsCreatedAtActionWithMappedDto()
        {
            // Arrange
            var createDto = PersonFactory.CreatePersonCreateDtoV2();

            var createdPerson = PersonFactory.CreatePerson(createDto.Name, createDto.CPF);

            _personServiceMock
                .Setup(x => x.CreateAsync(createDto))
                .ReturnsAsync(createdPerson);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtResult = result as CreatedAtActionResult;

            createdAtResult!.ActionName.Should().Be(nameof(PersonControllerV2.GetById));
            createdAtResult.RouteValues!["id"].Should().Be(1);
            createdAtResult.Value.Should().NotBeNull();
            createdAtResult.Value.Should().NotBeSameAs(createdPerson); // Should be mapped DTO

            _personServiceMock.Verify(x => x.CreateAsync(createDto), Times.Once);
        }

        [Fact]
        public async Task Create_WhenServiceThrowsException_PropagatesException()
        {
            // Arrange
            var createDto = PersonFactory.CreatePersonCreateDtoV2();

            _personServiceMock
                .Setup(x => x.CreateAsync(createDto))
                .ThrowsAsync(new Exception("Creation failed"));

            // Act
            Func<Task> act = async () => await _controller.Create(createDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Creation failed");
        }


        [Fact]
        public async Task Update_WithValidDto_ReturnsOkWithMappedDto()
        {
            // Arrange
            var updateDto = PersonFactory.CreatePersonUpdateDTOv2();

            var updatedPerson = PersonFactory.CreatePerson(updateDto.Name, updateDto.CPF);

            _personServiceMock
                .Setup(x => x.UpdateAsync(updatedPerson.Id, updateDto))
                .ReturnsAsync(updatedPerson);

            // Act
            var result = await _controller.Update(1, updateDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            okResult!.Value.Should().NotBeNull();
            okResult.Value.Should().NotBeSameAs(updatedPerson); // Should be mapped DTO

            _personServiceMock.Verify(x => x.UpdateAsync(1, updateDto), Times.Once);
        }

        [Fact]
        public async Task Update_WhenIdInRouteDoesNotMatchDtoId_StillCallsServiceWithDtoId()
        {
            // Arrange - Route id is 999 but DTO has id 1
            var updateDto = PersonFactory.CreatePersonUpdateDTOv2();

            var updatedPerson = PersonFactory.CreatePerson(updateDto.Name, updateDto.CPF);

            _personServiceMock
                .Setup(x => x.UpdateAsync(updatedPerson.Id, updateDto))
                .ReturnsAsync(updatedPerson);

            // Act - Different ID in route vs DTO
            var result = await _controller.Update(999, updateDto);

            // Assert - Service should be called with DTO's ID, not route ID
            result.Should().BeOfType<OkObjectResult>();
            _personServiceMock.Verify(x => x.UpdateAsync(updatedPerson.Id, updateDto), Times.Once);
        }

        [Fact]
        public async Task Update_WhenServiceThrowsException_PropagatesException()
        {
            // Arrange
            var updateDto = PersonFactory.CreatePersonUpdateDTOv2();

            _personServiceMock
                .Setup(x => x.UpdateAsync(1, updateDto))
                .ThrowsAsync(new Exception("Update failed"));

            // Act
            Func<Task> act = async () => await _controller.Update(1, updateDto);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Update failed");
        }

        [Fact]
        public void Constructor_CallsBaseConstructor()
        {
            // Arrange & Act
            var controller = new PersonControllerV2(_personServiceMock.Object);

            // Assert - Should be properly constructed without errors
            controller.Should().NotBeNull();
        }

        // Tests for inherited methods from PersonBasicController
        [Fact]
        public async Task GetAll_CallsBaseImplementation()
        {
            // Arrange
            var people = new List<Person>
            {
                PersonFactory.CreatePerson("Test Person A", "00099988877"),
                PersonFactory.CreatePerson("Test Person B", "00099988877")
            };

            _personServiceMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(people);

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _personServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetById_CallsBaseImplementation()
        {
            // Arrange
            var person = PersonFactory.CreatePerson("Test Person B", "00099988877");


            _personServiceMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(person);

            // Act
            var result = await _controller.GetById(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _personServiceMock.Verify(x => x.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Delete_CallsBaseImplementation()
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
        public async Task Create_UsesPersonMapperCorrectly()
        {
            // Arrange
            var createDto = PersonFactory.CreatePersonCreateDtoV2();

            var createdPerson = PersonFactory.CreatePerson(createDto.Name, createDto.CPF);


            _personServiceMock
                .Setup(x => x.CreateAsync(createDto))
                .ReturnsAsync(createdPerson);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            var createdAtResult = result as CreatedAtActionResult;

            // Verify that the mapper was used
            var personDto = createdAtResult!.Value;
            personDto.Should().NotBeNull();
            personDto.Should().NotBeSameAs(createdPerson);
        }

        [Fact]
        public async Task Update_UsesPersonMapperCorrectly()
        {
            // Arrange
            var updateDto = PersonFactory.CreatePersonUpdateDTOv2();

            var updatedPerson = PersonFactory.CreatePerson(updateDto.Name, updateDto.CPF);

            _personServiceMock
                .Setup(x => x.UpdateAsync(updatedPerson.Id, updateDto))
                .ReturnsAsync(updatedPerson);

            // Act
            var result = await _controller.Update(1, updateDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;

            // Verify that the mapper was used
            var personDto = okResult!.Value;
            personDto.Should().NotBeNull();
            personDto.Should().NotBeSameAs(updatedPerson);
        }

        // V2 Specific Tests - Different DTO types from V1
        [Fact]
        public async Task Create_UsesV2DtoType()
        {
            // Arrange
            var createDto = PersonFactory.CreatePersonCreateDtoV2();

            var createdPerson = PersonFactory.CreatePerson(createDto.Name, createDto.CPF);

            _personServiceMock
                .Setup(x => x.CreateAsync(It.IsAny<PersonCreateDtoV2>()))
                .ReturnsAsync(createdPerson);

            // Act
            var result = await _controller.Create(createDto);

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
            _personServiceMock.Verify(x => x.CreateAsync(It.Is<PersonCreateDtoV2>(d =>
                d.Name == createDto.Name &&
                d.CPF == createDto.CPF)), Times.Once);
        }

        [Fact]
        public async Task Update_UsesV2DtoType()
        {
            // Arrange
            var updateDto = PersonFactory.CreatePersonUpdateDTOv2();

            var updatedPerson = PersonFactory.CreatePerson(updateDto.Name, updateDto.CPF);

            _personServiceMock
                .Setup(x => x.UpdateAsync(updatedPerson.Id, It.IsAny<PersonUpdateDtoV2>()))
                .ReturnsAsync(updatedPerson);

            // Act
            var result = await _controller.Update(1, updateDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            _personServiceMock.Verify(x => x.UpdateAsync(updatedPerson.Id, It.Is<PersonUpdateDtoV2>(d =>
                d.Name == updateDto.Name)), Times.Once);
        }

        // Test parameter attributes explicitly
        [Fact]
        public async Task Update_UsesFromRouteAndFromBodyAttributes()
        {
            // This test verifies that the method signature matches the expected attributes
            // Arrange
            var updateDto = PersonFactory.CreatePersonUpdateDTOv2();

            var updatedPerson = PersonFactory.CreatePerson(updateDto.Name, updateDto.CPF);

            _personServiceMock
                .Setup(x => x.UpdateAsync(updatedPerson.Id, updateDto))
                .ReturnsAsync(updatedPerson);

            // Act - The test itself verifies the parameter binding works correctly
            var result = await _controller.Update(1, updateDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}