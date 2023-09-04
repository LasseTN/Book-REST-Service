using BusinessLogic.Interfaces;
using DataAccess.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {
    public class UserControl : IUserControl {

        private readonly IUserAccess _userAccess;
        public UserControl(IUserAccess userAccess) { 
            _userAccess = userAccess;
        }
        public async Task<bool> Create(User entity) {
            return await _userAccess.Create(entity);
        }

        public Task<User> Get(string id) {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User entity) {
            throw new NotImplementedException();
        }
    }
}
