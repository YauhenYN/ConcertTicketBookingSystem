using Microsoft.AspNetCore.Mvc;
using System;

namespace PL.Controllers
{
    public abstract class CustomControllerBase : ControllerBase
    {
        protected Guid? UserId { get => HttpContext.User.Identity.Name == null ? null : Guid.Parse(HttpContext.User.Identity.Name); }
    }
}
