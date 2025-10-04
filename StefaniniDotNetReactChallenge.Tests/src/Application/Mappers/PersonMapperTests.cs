using FluentAssertions;
using StefaniniDotNetReactChallenge.Application.Mappers;
using StefaniniDotNetReactChallenge.Tests.Helpers;

namespace StefaniniDotNetReactChallenge.Tests.Application;

public class PersonMapperTests
{
    [Fact]
    public void ToEntity_FromDTOs()
    {
        var createDTOv1 = PersonFactory.CreatePersonCreateDtoV1();
        var createDTOv2 = PersonFactory.CreatePersonCreateDtoV2();

        var updateDTOv1 = PersonFactory.CreatePersonUpdateDTOv1();
        var updateDTOv2 = PersonFactory.CreatePersonUpdateDTOv1();

        var resultCreateV1 = PersonMapper.ToEntity(createDTOv1);
        var resultCreateV2 = PersonMapper.ToEntity(createDTOv2);
        var resultUpdateV1 = PersonMapper.ToEntity(updateDTOv1);
        var resultUpdateV2 = PersonMapper.ToEntity(updateDTOv2);

        resultCreateV1.Should().NotBeNull();
        resultCreateV1.Name.Should().Be(createDTOv1.Name);

        resultCreateV2.Should().NotBeNull();
        resultCreateV2.Name.Should().Be(createDTOv2.Name);

        resultUpdateV1.Should().NotBeNull();
        resultUpdateV1.Name.Should().Be(updateDTOv1.Name);

        resultUpdateV2.Should().NotBeNull();
        resultUpdateV2.Name.Should().Be(updateDTOv2.Name);
    }

    [Fact]
    public void ToDTO_FromEntity()
    {
        var person = PersonFactory.CreatePerson(Guid.NewGuid().ToString(), "");

        var result = PersonMapper.ToDto(person);

        result.Should().NotBeNull();
        result.Name.Should().Be(person.Name);
    }
}