using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CandidateRepository
    {
        private Su25jlptmockTestDbContext _db;
        public CandidateRepository()
        {
            _db = new Su25jlptmockTestDbContext();
        }
        public List<Candidate> GetCandidates()
        {
            return _db.Candidates.ToList();
        }
    }
  
}
