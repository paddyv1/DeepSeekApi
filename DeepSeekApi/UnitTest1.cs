using DeepSeek.Core;
using DeepSeek.Core.Models;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Message = DeepSeek.Core.Models.Message;


namespace DeepSeekApi
{
    public class Tests
    {


        [Test]
        public async Task DeepSeekApiAsync()
        {
            //dotnet add package Ater.DeepSeek.Core

            //this is the api key MAKE SURE TO HIDE IT!
            string apikeyDeepSeek = "sk-ccffb664d9964511aa016db93e2e43ba";
            
            var client = new DeepSeekClient(apikeyDeepSeek);


            //couldnt find exact json formatter in the sdk, pretty sure they just convert to text
            //can ask the model ot provide a json format 
            //system is for model, user is user message
            //multiple requests to the api use caching according to docs,
            //cache per SystemMessage and UserMessage
            
            var request = new ChatRequest
            {
                Messages = [
                Message.NewSystemMessage("You are a teacher who marks childrens books, you need to focus on providing" +
                                           "feeback on spelling puntuation and grammar. Focus on using positive affirmations" +
                                           " when marking the students work, provide a response in a json friendly format with two params" +
                                           " the student name and the feedback as the other"),
                Message.NewSystemMessage("You love to use the word 'Fab!' when marking the work"),
                Message.NewUserMessage("Patrick's Work: the toad woh was very grumpy sad and angry when he was sent to jail. \"i cant believe you caught me!\" the toad said angriyl")

                ],
                // Specify the model
                //reasoner model might be more complex, costs twice as much to run, so far its been very cheap
                //only used 1 cent

                //Model = DeepSeekModels.ReasonerModel,
                Model = DeepSeekModels.ChatModel,
            };

            //request.Messages.Add();

            var chatResponse = await client.ChatAsync(request, new CancellationToken());

            if (chatResponse is null)
            {
                Console.WriteLine(client.ErrorMsg);
                Assert.Fail();
            }


            Console.WriteLine(chatResponse?.Choices.First().Message?.Content);



        }
    }
}