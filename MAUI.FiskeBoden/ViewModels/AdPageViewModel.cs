using System.ComponentModel;
using System.Runtime.CompilerServices;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Informatics.FiskeBoden.ViewModels
{
    public class AdPageViewModel : INotifyPropertyChanged
    {
        private readonly IBatchService _batchService;
        private readonly IChatGptService _chatGptService;
        private string _adText = string.Empty;
        
        public event PropertyChangedEventHandler? PropertyChanged;
        
        public string AdText
        {
            get => _adText;
            set { _adText = value; OnPropertyChanged(); }
        }

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name ?? string.Empty));

        public AdPageViewModel(IBatchService batchService, IChatGptService chatGptService)
        {
            _batchService = batchService ?? throw new ArgumentNullException(nameof(batchService));
            _chatGptService = chatGptService ?? throw new ArgumentNullException(nameof(chatGptService));
            AdText = "Enter a batch week number and click 'Generate Ad'";
        }

        public async Task GenerateAdForWeekAsync(int batchWeek)
        {
            AdText = "Generating ad...";
            try
            {
                var allBatches = await _batchService.GetBatchesAsync();
                var relevantBatches = allBatches.Where(b => b.BatchWeek == batchWeek).ToList();
                
                AdText = relevantBatches.Any()
                    ? await _chatGptService.GenerateAdForWeek(batchWeek, relevantBatches)
                    : "No batches found for this week.";
            }
            catch (Exception ex)
            {
                AdText = $"Error generating ad: {ex.Message}";
            }
        }
    }
}
