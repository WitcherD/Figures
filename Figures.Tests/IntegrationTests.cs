using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Figures.Contracts.Dto;
using Xunit;

namespace Figures.Tests
{
    public class IntegrationTests : IClassFixture<FiguresWebApplicationFactory>
    {
        private readonly FiguresWebApplicationFactory _factory;

        public IntegrationTests(FiguresWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CreateTriangleAsync_ValidParams_Should_Return_SuccessCode()
        {
            var content = JsonSerializer.Serialize(new
            {
                vertex1 = new { x = 0, y = 0 },
                vertex2 = new { x = 4, y = 0 },
                vertex3 = new { x = 2, y = 2 },
            });

            var client = _factory.CreateClient();
            var response = await client.PostAsync("figure/triangle", new StringContent(content, Encoding.UTF8, "application/json"));
            
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateTriangleAsync_MissingParams_Should_Return_BadRequest()
        {
            var content = JsonSerializer.Serialize(new
            {
                vertex1 = new { x = 0, y = 0 },
                vertex2 = new { x = 4, y = 0 },
            });

            var client = _factory.CreateClient();
            var response = await client.PostAsync("figure/triangle", new StringContent(content, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task CreateCircleAsync_ValidParams_Should_Return_SuccessCode()
        {
            var content = JsonSerializer.Serialize(new
            {
                center = new { x = 0, y = 0 },
                radius = 10
            });

            var client = _factory.CreateClient();
            var response = await client.PostAsync("figure/circle", new StringContent(content, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateCircleAsync_InvalidRadius_Should_Return_BadRequest()
        {
            var content = JsonSerializer.Serialize(new
            {
                center = new { x = 0, y = 0 },
                radius = -5
            });

            var client = _factory.CreateClient();
            var response = await client.PostAsync("figure/circle", new StringContent(content, Encoding.UTF8, "application/json"));

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetFigure_Should_Return_CreatedFigure()
        {
            var content = JsonSerializer.Serialize(new
            {
                vertex1 = new { x = 0, y = 0 },
                vertex2 = new { x = 4, y = 0 },
                vertex3 = new { x = 2, y = 2 },
            });

            var client = _factory.CreateClient();
            var response = await client.PostAsync("figure/triangle", new StringContent(content, Encoding.UTF8, "application/json"));

            var figure = await response.Content.ReadFromJsonAsync<Figure>();
            
            Assert.NotNull(figure);
            Assert.True(figure.FigureId > 0);

            response = await client.GetAsync($"figure/{figure.FigureId}");
            figure = await response.Content.ReadFromJsonAsync<Figure>();

            Assert.NotNull(figure);
            Assert.True(figure.FigureId > 0);
            Assert.Equal(4, figure.Area);
        }
    }
}