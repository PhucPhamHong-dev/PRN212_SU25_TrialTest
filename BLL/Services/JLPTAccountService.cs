using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class JLPTAccountService
    {
        //TẠO BIẾN PRIVATE 
        private JLPTAccountRepository _accountRepository;
        public JLPTAccountService()
        {
            _accountRepository = new JLPTAccountRepository();
        }
        //VIẾT CÁC HÀM XỬ LÝ 
        public Jlptaccount? GetJlptaccount(string email, string password)
        {
            return _accountRepository.GetJlptaccount(email, password);
        }
    }
}
