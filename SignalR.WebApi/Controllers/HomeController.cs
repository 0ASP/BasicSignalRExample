using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR.WebApi.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalR.WebApi.Controllers
{
    public class HomeController : Controller
    {


        IHubContext<FirstHub> _hubContext;
        public HomeController(IHubContext<FirstHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
