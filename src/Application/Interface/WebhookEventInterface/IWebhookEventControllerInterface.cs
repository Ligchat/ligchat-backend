using LigChat.Backend.Domain.DTOs.WebhookEventDto;
using Microsoft.AspNetCore.Mvc;

namespace LigChat.Backend.Application.Interface.WebhookEventInterface
{
    public interface IWebhookEventControllerInterface
    {
        IActionResult GetAll();
        IActionResult GetById(int id);
        IActionResult Save(CreateWebhookEventRequestDTO webhookEvent);
        IActionResult Update(int id, UpdateWebhookEventRequestDTO webhookEvent);
        IActionResult Delete(int id);
    }
}
