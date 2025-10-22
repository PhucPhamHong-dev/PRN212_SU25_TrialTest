using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class MockTestRepository
    {
        private Su25jlptmockTestDbContext _db;
        public MockTestRepository()
        {
         
            _db = new Su25jlptmockTestDbContext();
        }
        public List <MockTest> GetMockTest()
        {
            return _db.MockTests.Include(x => x.Candidate).ToList();
        }
        public List<MockTest> GetMockTest(string keyword)
        {
            return _db.MockTests.Where(x => x.TestTitle.ToLower().Contains(keyword.ToLower())).ToList();
        }
        public void DeleteMockTest(MockTest test) { 
            _db.MockTests.Remove(test);
            _db.SaveChanges();
         }
        public void AddMockTest(MockTest test)
        {
            _db.MockTests.Add(test);
            _db.SaveChanges();
        }
        public int GetNextTestId()
        {
            if (_db.MockTests.Any())
                return _db.MockTests.Max(t => t.TestId) + 1;
            return 1;
        }
        public void UpdateMockTest(MockTest mockTest)
        {
            var existing = _db.MockTests.FirstOrDefault(x => x.TestId == mockTest.TestId);
            if (existing == null)
            {
                throw new Exception("Mock test not found.");
            }

            existing.TestTitle = mockTest.TestTitle;
            existing.SkillArea = mockTest.SkillArea;
            existing.StartTime = mockTest.StartTime;
            existing.EndTime = mockTest.EndTime;
            existing.CandidateId = mockTest.CandidateId;
            existing.Score = mockTest.Score;

            _db.SaveChanges();
        }
    }
}
