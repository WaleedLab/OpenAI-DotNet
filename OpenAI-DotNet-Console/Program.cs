using OpenAI;
using OpenAI.Chat;

var auth = OpenAIAuthentication.LoadFromEnv();
var settings = new OpenAIClientSettings(/* your custom settings if using Azure OpenAI */);
var openAIClient = new OpenAIClient(auth, settings);

using var api = new OpenAIClient(auth);

var messages = new List<Message>
{
    //new Message(Role.System, "You are a helpful assistant."),
    //new Message(Role.User, "Who won the world series in 2020?"),
    //new Message(Role.Assistant, "The Los Angeles Dodgers won the World Series in 2020."),
    new Message(Role.User, "write small example c# code."),
};

var chatRequest = new ChatRequest(messages);
var response = await api.ChatEndpoint.StreamCompletionAsync(chatRequest, partialResponse =>
{
    Console.Write(partialResponse.FirstChoice.Delta.ToString());
});

var choice = response.FirstChoice;
Console.WriteLine($"[{choice.Index}] {choice.Message.Role}: {choice.Message} | Finish Reason: {choice.FinishReason}");