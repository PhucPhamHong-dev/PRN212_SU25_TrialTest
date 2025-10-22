using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CandidateService
    {
        private CandidateRepository _CandidateRepository;
        public CandidateService()
        {
            _CandidateRepository = new CandidateRepository();

        }
        public List<Candidate> GetCandidates()
        {
            return _CandidateRepository.GetCandidates();
        }
    }
}
