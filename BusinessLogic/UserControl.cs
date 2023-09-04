using BusinessLogic.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic {
    public class UserControl : IUserControl {
        public Task<bool> Create(User entity) {
            throw new NotImplementedException();
        }

        public Task<User> Get(string id) {
            throw new NotImplementedException();
        }

        public Task<bool> Update(User entity) {
            throw new NotImplementedException();
        }
    }
}
