using System;
using System.Threading.Tasks;
using API.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AnswerQuestionCommand = API.Commands.AnswerQuestionCommand;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        // GET api/questions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetQuestionQuery {Id = id});
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/questions/answers
        [HttpPost("answers")]
        public async Task<IActionResult> Post([FromBody] AnswerQuestionCommand command)
        {
            var questionId = await _mediator.Send(command);
            return Created($"/api/questions/{questionId}", null);
        }
    }
}