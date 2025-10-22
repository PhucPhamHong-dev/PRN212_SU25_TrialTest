using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class JLPTAccountRepository
    {
        //Bước 1: Tạo BIẾN private (dùng để gọi tới database)
        private Su25jlptmockTestDbContext _db;
        //BƯỚC 2: Dựng Constructor và NEW đến biến đó 
        public JLPTAccountRepository()
        {
            _db = new Su25jlptmockTestDbContext();
        }
        //BƯỚC 3: VIẾT CÁC HÀM XỬ LÝ
        public Jlptaccount? GetJlptaccount(string Email, string Password)
        {
            //XUỐNG DATABASE VÀO BẢNG ACCOUNT TÌM KIẾM 
            return _db.Jlptaccounts.FirstOrDefault(x => x.Email == Email && x.Password == Password);

        }
    }
}
