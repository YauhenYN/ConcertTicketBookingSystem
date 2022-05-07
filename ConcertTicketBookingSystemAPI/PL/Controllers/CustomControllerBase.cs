using Microsoft.AspNetCore.Mvc;
using System;

namespace PL.Controllers
{
    public abstract class CustomControllerBase : ControllerBase
    {
        protected Guid UserId { get => Guid.Parse(HttpContext.User.Identity.Name); }
    }
}
