using Microsoft.AspNetCore.Mvc;
using newsletterservice.Services;
using newsletterservice.Models;

namespace newsletterservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsletterController : ControllerBase
    {
        private readonly NewsletterService _newsletterService;

        public NewsletterController(NewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }
    }
}    