using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Figures.Contracts.Dto;
using Figures.Db;
using Figures.Math;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Point = Figures.Math.Point;

namespace Figures.Api.Controllers
{
    [ApiController]
    [Route("figure")]
    public class FiguresController : ControllerBase
    {
        private readonly FiguresDbContext _dbContext;

        public FiguresController(FiguresDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("triangle", Name = "createTriangle")]
        public async Task<ActionResult<Figure>> CreateTriangleAsync(
            [FromBody] CreateTriangleRequest request,
            CancellationToken cancellationToken)
        {
            var entity = new Db.Entities.Figure
            {
                FigureType = "triangle",
                Object = JsonSerializer.Serialize(request),
                Area = AreasCalculator.CalculatePolygonArea(
                    new Point(request.Vertex1.X, request.Vertex1.Y),
                    new Point(request.Vertex2.X, request.Vertex2.Y),
                    new Point(request.Vertex3.X, request.Vertex3.Y))
            };
            
            await _dbContext.Figures.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return await GetFigureAsync(entity.FigureId, cancellationToken);
        }

        [HttpPost("circle", Name = "createCircle")]
        public async Task<ActionResult<Figure>> CreateCircleAsync(
            [FromBody] CreateCircleRequest request,
            CancellationToken cancellationToken)
        {
            var figure = new Db.Entities.Figure
            {
                FigureType = "circle",
                Object = JsonSerializer.Serialize(request),
                Area = AreasCalculator.CalculateCircleArea(request.Radius)
            };
            
            await _dbContext.Figures.AddAsync(figure, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return await GetFigureAsync(figure.FigureId, cancellationToken);
        }

        [HttpGet("{id:int}", Name = "getFigure")]
        public async Task<ActionResult<Figure>> GetFigureAsync(
            [FromRoute] int id,
            CancellationToken cancellationToken)
        {
            var figure = await _dbContext.Figures
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.FigureId == id, cancellationToken);

            if (figure == null)
            {
                return NotFound();
            }
            
            var result = new Figure
            {
                FigureId = figure.FigureId, 
                Area = figure.Area
            };
            return Ok(result);
        }
    }
}
