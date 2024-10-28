namespace tests_.src.Web.Controller
{
    using global::tests_.src.Application.Interface.Message.Ligchat.Application.Interfaces;
    using global::tests_.src.Domain.Entities;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    namespace Ligchat.Web.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class MessagesController : ControllerBase
        {
            private readonly IMessageService _messageService;

            public MessagesController(IMessageService messageService)
            {
                _messageService = messageService;
            }

            // GET: api/messages?whatsappContactId=2
            [HttpGet]
            public ActionResult<IEnumerable<Messeageging>> GetAll(int whatsappContactId)
            {
                var messages = _messageService.GetAllByContactId(whatsappContactId);
                return Ok(messages);
            }

            // GET: api/messages/2
            [HttpGet("{id}")]
            public ActionResult<Messeageging> GetById(int id)
            {
                var message = _messageService.GetById(id);
                if (message == null)
                {
                    return NotFound();
                }
                return Ok(message);
            }

            // POST: api/messages
            [HttpPost]
            public ActionResult<Messeageging> Create(Messeageging message)
            {
                var createdMessage = _messageService.Create(message);
                return CreatedAtAction(nameof(GetById), new { id = createdMessage.Id }, createdMessage);
            }

            // PUT: api/messages/2
            [HttpPut("{id}")]
            public IActionResult Update(int id, Messeageging message)
            {
                if (id != message.Id)
                {
                    return BadRequest("The message ID does not match.");
                }

                var updated = _messageService.Update(message);
                if (!updated)
                {
                    return NotFound();
                }

                return NoContent();
            }

            // DELETE: api/messages/2
            [HttpDelete("{id}")]
            public IActionResult Delete(int id)
            {
                var deleted = _messageService.Delete(id);
                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
        }
    }

}
