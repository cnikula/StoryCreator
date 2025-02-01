using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace StoryCreator.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string StoryContent { get; set; }
        public string Title { get; set; }

        [BindProperty]
        public StoryDetails Details { get; set; } = new StoryDetails();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnPost()
        {
            // Testing with MIT Azure Open AI
            //string endpoint = "https://mitai.openai.azure.com/";
            //string key = "e67e1ba43c3244d5bd618fbc2a8f282c";
            //string deploymentName = "turb035mitai";

            // Testing with Adam AI Azure
            string endpoint = "https://graiatestingdev001.openai.azure.com/";
            string key = "AI API ENDPOIT GOS HERE";
            string deploymentName = "aiaturbo35";

            OpenAIClient client = new(new Uri(endpoint), new AzureKeyCredential(key));

            string prompt = $"Write me a story called {Details.Title} in a {Details.Tone} tone about a " +
                $"{Details.Animal} named {Details.Name} who lives in a {Details.Environment}.";

            ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, "You are a helpful AI Bot."),
                    new ChatMessage(ChatRole.User, prompt)
                }
            };

            // Send request to Azure OpenAI model
            ChatCompletions chatCompletionsResponse = client.GetChatCompletions(deploymentName, chatCompletionsOptions);

            StoryContent = chatCompletionsResponse.Choices[0].Message.Content;
        }
    }

    public class StoryDetails
    {
        public string Title { get; set; }
        public string Tone { get; set; }
        public string Name { get; set; }
        public string Animal { get; set; }
        public string Environment { get; set; }

    }
}