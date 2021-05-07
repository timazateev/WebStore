using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;

namespace WebStore.ServiceHosting.Controllers.Identity
{
    [Route(WebAPI.Identity.User)]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserStore<User, Role, WebStoreContext> _UserStore;

        public UsersController(WebStoreContext db)
        {
            _UserStore = new UserStore<User, Role, WebStoreContext>(db);
            //_UserStore.AutoSaveChanges = false;  //default true
        }
    }
}
