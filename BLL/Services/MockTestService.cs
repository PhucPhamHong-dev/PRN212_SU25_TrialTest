using DAL.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class MockTestService
    {
        private MockTestRepository _MockTestRepository;
        public MockTestService()
        {
            _MockTestRepository = new MockTestRepository();
        }
        public List<MockTest> GetMockTest()
        {
            return _MockTestRepository.GetMockTest();
        }
        public List<MockTest> GetMockTest(string keyword)
        {
            return _MockTestRepository.GetMockTest(keyword);
        }
        public void DeleteMockTest(MockTest test) {
            _MockTestRepository.DeleteMockTest(test);
        }
        public void AddMockTest(MockTest Test)
        {
            _MockTestRepository.AddMockTest(Test);
        }
        public int GetNextTestId()
        {
            return _MockTestRepository.GetNextTestId();
        }
        public void UpdateMockTest(MockTest test)
        {
            _MockTestRepository.UpdateMockTest(test);
        }
    }
}

