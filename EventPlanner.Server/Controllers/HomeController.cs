using EventPlanner.Server.Authorization;
using EventPlanner.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        #region Fields

        private readonly ILogger<HomeController> logger;

        #endregion Fields

        #region Constructors

        public HomeController(ILogger<HomeController> logger)
        { 
            this.logger = logger;
        }

        #endregion Constructors

        #region Public Methods

        [Authorize]
        [HttpGet]
        public List<ImageContent> GetContent()
        {
            List<ImageContent> images =
            [
                new () 
                {
                    Id = Guid.NewGuid(),
                    Source = "./src/assets/img/home/lake.jpg",
                    AltText = "Banner One",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    Source = "./src/assets/img/home/landscape.jpg",
                    AltText = "Banner Two",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    Source = "./src/assets/img/home/sunset.jpg",
                    AltText = "Banner Three",
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                }
            ];

            return images;
        }

        #endregion Public Methods
    }
}