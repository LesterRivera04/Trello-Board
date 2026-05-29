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
        public async Task AddAsync_Should_Call_Services_AddAsync()
        {
            // Arrange
            var mockService = new Mock<IUserStoryRepository>();
            mockService.Setup(s => s.AddAsync(It.IsAny<UserStory>())).Returns(Task.CompletedTask);
            var service = new UserStoryServices(mockService.Object);

            var newStory = new CreateUserStoryDto
            {
                Titulo = "titulillo a ver que",
                Descripcion = "descripcion prueba",
                AsignadoA = 1
            };

            //Act
            await service.AddAsync(newStory);

            //Assert
            mockService.Verify(s => s.AddAsync(It.Is<UserStory>(dto =>
                dto.Titulo == newStory.Titulo &&
                dto.Descripcion == newStory.Descripcion &&
                dto.AsignadoA == newStory.AsignadoA
            )), Times.Once);

            mockService.Verify(s => s.GetAllAsync(), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_Todos_los_User_Story()
        {
            // Arrange
            var mockService = new Mock<IUserStoryRepository>();
            var expectedStories = new List<UserStoryDto>
            {
                new UserStoryDto { Id = 1, Titulo = "Historia 1", Descripcion = "Descripción 1", AsignadoA = 1, Estado = "Backlog", Estimacion = 3 },
                new UserStoryDto { Id = 2, Titulo = "Historia 2", Descripcion = "Descripción 2", AsignadoA = 2, Estado = "InProgress", Estimacion = 5 }
            };
            mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(expectedStories);
            var service = new UserStoryServices(mockService.Object);
            // Act
            var result = await service.GetAllAsync();
            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("Historia 1", result.ElementAt(0).Titulo);
            Assert.Equal("Historia 2", result.ElementAt(1).Titulo);
            Assert.Equal(expectedStories.Count, result.Count());
            Assert.Equal(expectedStories[0].Titulo, result.First().Titulo);
        }

        [Fact]
        public async Task AddAsync_Should_Call_HttpClient_GetFromJsonAsync()
        {
            // Arrange
            var mockHttpClient = new Mock<HttpClient>();
            mockHttpClient.Setup(c => c.GetFromJsonAsync<int>("http://localhost:5205/estimate"))
                .ReturnsAsync(5);
            var service = new UserStoryServices(null, mockHttpClient.Object);
            var newStory = new CreateUserStoryDto
            {
                Titulo = "titulillo a ver que",
                Descripcion = "descripcion prueba",
                AsignadoA = 1
            };
            // Act
            await service.AddAsync(newStory);
            // Assert
            mockHttpClient.Verify(c => c.GetFromJsonAsync<int>("http://localhost:5205/estimate"), Times.Once);
        }
    }
}
