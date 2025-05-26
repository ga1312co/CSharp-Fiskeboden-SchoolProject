using Microsoft.Extensions.Configuration;
using OpenAI.Chat;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Informatics.FiskeBoden.Interfaces;
using Informatics.FiskeBoden.Models;

namespace Informatics.FiskeBoden.Services
{
    public class ChatGptService : IChatGptService
    {
        private readonly ChatClient _chatClient;

        public ChatGptService(IConfiguration configuration)
        {
            string apiKey = configuration["OpenAI:ApiKey"] ?? throw new ArgumentNullException("ApiKey cannot be null");
            // Initialize with gpt-4o-mini model as in professor's example
            _chatClient = new ChatClient("gpt-4o-mini", apiKey);
        }

        public async Task<string> GenerateAdForWeek(int batchWeek, List<Batch> batches)
        {
            if (batches == null || batches.Count == 0)
                return "No batches found for this week.";

            // Correctly access relevant Batch properties through navigation properties
            var batchDescriptions = string.Join(", ", batches.Select(b => 
                $"{b.Product.ProductName} from {b.Supplier.SupplierName} " +
                $"(Origin: {b.BatchOrigin}, Price: {b.BatchPrice:C}, Quantity: {b.BatchQuantity})."
            ));

            // Create the system and user prompt combined
            string fullPrompt = $"You are a creative copywriter that generates engaging ads for locally produced fish. " +
                               $"Create an ad for these fish batches from week {batchWeek}: {batchDescriptions}";

            // Await the asynchronous call to CompleteChat
            ChatCompletion completion = await _chatClient.CompleteChatAsync(fullPrompt);
            
            string response = completion.Content[0].Text;

            return response;
        }
    }
}