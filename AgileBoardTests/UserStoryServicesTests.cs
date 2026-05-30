using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TrelloBoard.API.Services;
using TrelloBoard.API.DTO;
using System.Net.Http.Json;
using TrelloBoard.API.Repository;
using TrelloBoard.API.Models;

namespace AgileBoardTests
{
    public class UserStoryServicesTests
    {
        // prueba sobre creacion de una historia de usuario, validando que se llame al repositorio para guardar la historia

        [Fact]
        public async Task AddAsync_Should_Create_UserStory_And_Save_It()
        {
            // Arrange
            var mockRepository = new Mock<IUserStoryRepository>();
            mockRepository.Setup(s => s.AddAsync(It.IsAny<UserStory>())).Returns(Task.CompletedTask);
            var service = new UserStoryServices(mockRepository.Object);

            var newStory = new CreateUserStoryDto
            {
                Titulo = "titulillo a ver que",
                Descripcion = "descripcion prueba",
                AsignadoA = 1
            };

            //Act
            await service.AddAsync(newStory);

            //Assert
            mockRepository.Verify(s => s.AddAsync(It.Is<UserStory>(dto =>
                dto.Titulo == newStory.Titulo &&
                dto.Descripcion == newStory.Descripcion &&
                dto.AsignadoA == newStory.AsignadoA
            )), Times.Once);

            mockRepository.Verify(s => s.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_UserStories()
        {
            // Arrange   -----> se crea un mock del repositorio, se configura para que devuelva una lista de historias de usuario, y se instancia el servicio con el mock
            var mockRepository = new Mock<IUserStoryRepository>();
            var expectedStories = new List<UserStory>
            {
                new UserStory { Id = 1, Titulo = "Historia 1", Descripcion = "Descripción 1", AsignadoA = 1, Estado = UserStoryState.Backlog, Estimacion = 3 },
                new UserStory { Id = 2, Titulo = "Historia 2", Descripcion = "Descripción 2", AsignadoA = 2, Estado = UserStoryState.InProgress, Estimacion = 5 }
            };
            mockRepository.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedStories);
            var service = new UserStoryServices(mockRepository.Object);
            // Act
            var result = await service.GetAllAsync();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Historia 1", result.ElementAt(0).Titulo);
            Assert.Equal("Historia 2", result.ElementAt(1).Titulo);
            Assert.Equal(expectedStories.Count, result.Count());                 //otra manera de validar el resultado, comparando la cantidad de historias devueltas con la cantidad esperada
            Assert.Equal(expectedStories[0].Titulo, result.First().Titulo);      //otra manera de validar el resultado, comparando el título de la primera historia devuelta con el título de la primera historia esperada
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_UserStory_When_Exists()
        {
            // Arrange
            var mockRepository = new Mock<IUserStoryRepository>();

            var userStory = new UserStory
            {
                Id = 1,
                Titulo = "Historia 1",
                Descripcion = "Descripción 1",
                AsignadoA = 1,
                Estado = UserStoryState.Backlog,
                Estimacion = 3
            };

            mockRepository
                .Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(userStory);

            var service = new UserStoryServices(mockRepository.Object);

            // Act
            var result = await service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Historia 1", result.Titulo);
            Assert.Equal("Descripción 1", result.Descripcion);
            Assert.Equal(1, result.AsignadoA);
            Assert.Equal("Backlog", result.Estado);
            Assert.Equal(3, result.Estimacion);

            mockRepository.Verify(r => r.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_Should_Return_Null_When_UserStory_Does_Not_Exist()
        {
            // Arrange
            var mockRepository = new Mock<IUserStoryRepository>();

            mockRepository
                .Setup(r => r.GetByIdAsync(999))
                .ReturnsAsync((UserStory?)null);

            var service = new UserStoryServices(mockRepository.Object);

            // Act
            var result = await service.GetByIdAsync(999);

            // Assert
            Assert.Null(result);

            mockRepository.Verify(r => r.GetByIdAsync(999), Times.Once);
        }
    }
}
