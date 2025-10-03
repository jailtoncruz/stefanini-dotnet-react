using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using StefaniniDotNetReactChallenge.Application.Services;
using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Domain.Interfaces;
using StefaniniDotNetReactChallenge.Application.DTOs;

namespace StefaniniDotNetReactChallenge.Tests.Application
{
    public class PersonServiceTests
    {
        private readonly Mock<IPersonRepository> _repoMock;
        private readonly PersonService _service;

        public PersonServiceTests()
        {
            _repoMock = new Mock<IPersonRepository>();
            _service = new PersonService(_repoMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnListOfPersons()
        {
            var persons = new List<Person>
            {
                PersonFactory("Jailton", "3183918381"),
                PersonFactory("Cruz", "3183918381")
            };
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(persons);

            var result = await _service.GetAllAsync();

            result.Should().HaveCount(2);
            result.Should().ContainSingle(p => p.Name == "Jailton");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectPerson()
        {
            var person = PersonFactory("Cruz", "3183918381");
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(person);

            var result = await _service.GetByIdAsync(1);

            result.Should().NotBeNull();
            result!.Name.Should().Be("Cruz");
        }

        [Fact]
        public async Task CreateAsync_V1_ShouldAddPerson()
        {
            var dto = new PersonCreateDtoV1 { Name = "Charlie" };

            var result = await _service.CreateAsync(dto);

            result.Name.Should().Be("Charlie");
            _repoMock.Verify(r => r.AddAsync(It.IsAny<Person>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_V1_ShouldUpdatePerson()
        {
            var dto = new PersonUpdateDtoV1 { Name = "Updated", };
            var updatedPerson = PersonFactory("Updated", "3137137131");
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Person>())).ReturnsAsync(updatedPerson);

            var result = await _service.UpdateAsync(dto);

            result.Name.Should().Be("Updated");
            _repoMock.Verify(r => r.UpdateAsync(It.IsAny<Person>()), Times.Once);
        }

        [Fact]
        public async Task DeleteByIdAsync_ShouldCallRepositoryDelete()
        {
            await _service.DeleteByIdAsync(5);

            _repoMock.Verify(r => r.DeleteByIdAsync(5), Times.Once);
        }

        private Person PersonFactory(string Name, string CPF)
        {
            return new Person
            {
                Id = 1,
                Name = Name,
                BirthDay = new DateOnly(),
                CPF = CPF,
                CreatedAt = new DateTime(),
                UpdatedAt = new DateTime()
            };
        }
    }
}
