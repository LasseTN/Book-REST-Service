using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_REST_Service.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IUserControl _userControl;

        public UserController(IUserControl userControl) {
            _userControl = userControl;
        }
    }
}
