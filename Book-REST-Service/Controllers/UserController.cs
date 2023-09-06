using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Book_REST_Service.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IUserControl _userControl;

        public UserController(IUserControl userControl) {
            _userControl = userControl;
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<bool>> CreateUser([FromBody] User userToCreate) {
            ActionResult<bool> foundResult;
            bool insertedOk = false;

            if (userToCreate != null) {
                insertedOk = await _userControl.Create(userToCreate);

                if (insertedOk == true) {
                    foundResult = Ok(insertedOk);
                } else {
                    return BadRequest();
                }
            } else {
                foundResult = new BadRequestResult();
            }

            return foundResult;
        }

    }
}
