using BLL.Services;
using DAL.Entities;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PE_PRN212_SU25_PHAM_HONG_PHUC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool IsAdmin => _currentAccount?.Role == 1;
        private bool IsManager => _currentAccount?.Role == 2;
        private bool CanCreate => IsAdmin || IsManager;   
        private bool CanUpdate => IsAdmin || IsManager;   
        private bool CanDelete => IsAdmin;                
        private  Jlptaccount? _currentAccount;
        private MockTestService _MockTestService;
        private CandidateService _CandidateService;
        private MockTest? _selectedMockTest;

        public MainWindow(Jlptaccount? currentAccount = null)
        {
            InitializeComponent();
            _currentAccount = currentAccount;
            _MockTestService = new MockTestService();
            _CandidateService = new CandidateService();
            LoadMockTest();
            LoadCandidates();
            //ApplyAuthorization();

        }
        //private void ApplyAuthorization()
        //{
        //    bool isAdmin = _currentAccount?.Role == 1;
        //    btnDelete.IsEnabled = isAdmin;
        //}

        private void LoadMockTest()
        {
            dgMockTest.ItemsSource = _MockTestService.GetMockTest();
        }
        private void LoadCandidates()
        {
            cbCandidate.ItemsSource=
                _CandidateService.GetCandidates();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string keyword= txtSearch.Text;
            //Nhờ service tìm kiếm
            //Sau đó đổ lên DataGrid
            dgMockTest.ItemsSource=
                _MockTestService.GetMockTest(keyword);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAccount?.Role != 1)
            {
                MessageBox.Show("You do not have permission to delete items.",
                    "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                var result = MessageBox.Show("DO YOU WANT DELETE",
                    "CẢNH BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (dgMockTest.SelectedItem is MockTest test)
                {
                    _MockTestService.DeleteMockTest(test);
                    LoadMockTest();
                }
            }

            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAccount?.Role != 1 && _currentAccount?.Role != 2)
            {
                MessageBox.Show("You do not have permission to update items.",
                    "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!ValidateInput()) return;

            // parse chuỗi "HH:mm" thành TimeOnly
            if (!TimeOnly.TryParseExact(txtStartTime.Text.Trim(), "HH:mm", out var startTime) ||
                !TimeOnly.TryParseExact(txtEndTime.Text.Trim(), "HH:mm", out var endTime))
            {
                MessageBox.Show("Please enter valid time in HH:mm format");
                return;
            }

            if (startTime >= endTime)
            {
                MessageBox.Show("Start Time must be earlier than End Time.");
                return;
            }

            var newTest = new MockTest
            {
                TestId = _MockTestService.GetNextTestId(),
                TestTitle = txtTestTitle.Text.Trim(),
                SkillArea = txtSkillArea.Text.Trim(),
                StartTime = startTime,
                EndTime = endTime,
                CandidateId = (int)cbCandidate.SelectedValue,
                Score = int.Parse(txtScore.Text.Trim())
            };

            _MockTestService.AddMockTest(newTest);
            LoadMockTest();
            MessageBox.Show("MockTest added successfully!");
        }
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTestTitle.Text) ||
                string.IsNullOrWhiteSpace(txtSkillArea.Text) ||
                string.IsNullOrWhiteSpace(txtStartTime.Text) ||
                string.IsNullOrWhiteSpace(txtEndTime.Text) ||
                cbCandidate.SelectedValue == null ||
                string.IsNullOrWhiteSpace(txtScore.Text))
            {
                MessageBox.Show("All fields are required!");
                return false;
            }

            if (!int.TryParse(txtScore.Text, out var score) || score < 0)
            {
                MessageBox.Show("Score must be a valid non-negative number.");
                return false;
            }

            if (txtTestTitle.Text.Length < 5 || txtTestTitle.Text.Length > 150)
            {
                MessageBox.Show("Test Title must be between 5 and 150 characters.");
                return false;
            }

            var regex = new Regex(@"^(?:[A-Z0-9][a-zA-Z0-9]*(?: [A-Z0-9][a-zA-Z0-9]*)*)$");
            if (!regex.IsMatch(txtTestTitle.Text))
            {
                MessageBox.Show("Each word must start with a capital letter or number, and not contain $, %, ^, @.");
                return false;
            }

            return true;
        }

        private void dgMockTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMockTest.SelectedItem is MockTest m)
            {
                _selectedMockTest = m;                       
                txtTestTitle.Text = m.TestTitle;
                txtSkillArea.Text = m.SkillArea;
                txtStartTime.Text = m.StartTime.ToString("HH:mm");
                txtEndTime.Text = m.EndTime.ToString("HH:mm");
                cbCandidate.SelectedValue = m.CandidateId;
                txtScore.Text = m.Score?.ToString(CultureInfo.InvariantCulture) ?? string.Empty;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_currentAccount?.Role != 1 && _currentAccount?.Role != 2)
            {
                MessageBox.Show("You do not have permission to update items.",
                    "Permission Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_selectedMockTest == null)
            {
                MessageBox.Show("Please select a mock test to update.");
                return;
            }

            if (!ValidateInput()) return;

            // parse time từ textbox
            if (!TimeOnly.TryParseExact(txtStartTime.Text.Trim(), "HH:mm", out var startTime) ||
                !TimeOnly.TryParseExact(txtEndTime.Text.Trim(), "HH:mm", out var endTime))
            {
                MessageBox.Show("Time must be HH:mm (e.g. 08:30).");
                return;
            }
            if (startTime >= endTime)
            {
                MessageBox.Show("Start Time must be earlier than End Time.");
                return;
            }

            var updated = new MockTest
            {
                TestId = _selectedMockTest.TestId,                 
                TestTitle = txtTestTitle.Text.Trim(),
                SkillArea = txtSkillArea.Text.Trim(),
                StartTime = startTime,
                EndTime = endTime,
                CandidateId = Convert.ToInt32(cbCandidate.SelectedValue),
                Score = int.Parse(txtScore.Text.Trim())
            };

            _MockTestService.UpdateMockTest(updated);               
            LoadMockTest();
            MessageBox.Show("Mock test updated successfully!");
        }
    }
}
