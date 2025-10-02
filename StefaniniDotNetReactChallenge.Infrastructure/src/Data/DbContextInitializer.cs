using StefaniniDotNetReactChallenge.Domain.Entities;
using StefaniniDotNetReactChallenge.Infrastructure.Data;

namespace StefaniniDotNetReactChallenge.Infrastructure.Persistence;

public static class DbContextInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await context.Database.EnsureCreatedAsync();

        if (context.People.Any())
            return;

        var people = new List<Person>
        {
            new Person
            {
                Id = 1,
                Name = "Maria Silva",
                Gender = "Feminino",
                Email = "maria.silva@example.com",
                BirthDay = new DateOnly(1999, 11, 20),
                Nationality = "Brasileira",
                PlaceOfBirth = "São Paulo",
                CPF = "12345678901",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 2,
                Name = "João Souza",
                Gender = "Masculino",
                Email = "joao.souza@example.com",
                BirthDay = new DateOnly(1987, 4, 15),
                Nationality = "Brasileiro",
                PlaceOfBirth = "Rio de Janeiro",
                CPF = "23456789012",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 3,
                Name = "Ana Oliveira",
                Gender = "Feminino",
                Email = "ana.oliveira@example.com",
                BirthDay = new DateOnly(1995, 8, 5),
                Nationality = "Brasileira",
                PlaceOfBirth = "Belo Horizonte",
                CPF = "34567890123",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 4,
                Name = "Carlos Pereira",
                Gender = "Masculino",
                Email = "carlos.pereira@example.com",
                BirthDay = new DateOnly(1990, 2, 10),
                Nationality = "Brasileiro",
                PlaceOfBirth = "Curitiba",
                CPF = "45678901234",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 5,
                Name = "Fernanda Costa",
                Gender = "Feminino",
                Email = "fernanda.costa@example.com",
                BirthDay = new DateOnly(2000, 6, 30),
                Nationality = "Brasileira",
                PlaceOfBirth = "Salvador",
                CPF = "56789012345",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 6,
                Name = "Paulo Henrique",
                Gender = "Masculino",
                Email = "paulo.henrique@example.com",
                BirthDay = new DateOnly(1992, 9, 12),
                Nationality = "Brasileiro",
                PlaceOfBirth = "Fortaleza",
                CPF = "67890123456",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 7,
                Name = "Juliana Lima",
                Gender = "Feminino",
                Email = "juliana.lima@example.com",
                BirthDay = new DateOnly(1985, 3, 3),
                Nationality = "Brasileira",
                PlaceOfBirth = "Recife",
                CPF = "78901234567",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 8,
                Name = "Rafael Almeida",
                Gender = "Masculino",
                Email = "rafael.almeida@example.com",
                BirthDay = new DateOnly(1998, 12, 25),
                Nationality = "Brasileiro",
                PlaceOfBirth = "Brasília",
                CPF = "89012345678",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 9,
                Name = "Camila Rocha",
                Gender = "Feminino",
                Email = "camila.rocha@example.com",
                BirthDay = new DateOnly(1994, 5, 18),
                Nationality = "Brasileira",
                PlaceOfBirth = "Porto Alegre",
                CPF = "90123456789",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 10,
                Name = "Bruno Fernandes",
                Gender = "Masculino",
                Email = "bruno.fernandes@example.com",
                BirthDay = new DateOnly(1989, 7, 7),
                Nationality = "Brasileiro",
                PlaceOfBirth = "Manaus",
                CPF = "01234567890",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 11,
                Name = "Larissa Martins",
                Gender = "Feminino",
                Email = "larissa.martins@example.com",
                BirthDay = new DateOnly(1997, 1, 29),
                Nationality = "Brasileira",
                PlaceOfBirth = "Goiânia",
                CPF = "11223344556",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Person
            {
                Id = 12,
                Name = "Diego Santos",
                Gender = "Masculino",
                Email = "diego.santos@example.com",
                BirthDay = new DateOnly(1993, 10, 14),
                Nationality = "Brasileiro",
                PlaceOfBirth = "Florianópolis",
                CPF = "22334455667",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        };

        context.People.AddRange(people);
        await context.SaveChangesAsync();
    }
}
