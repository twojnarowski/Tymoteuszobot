namespace Tymoteuszobot.Services;

using System.Text.Json;

internal class JokeService
{
    public static async Task<string> GetJoke()
    {
        HttpClient client = new();
        string webApiUrl = File.ReadAllText("JokeAPIURL.txt");

        string joke = "Error 404";
        HttpResponseMessage response = await client.GetAsync(webApiUrl);
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadAsStringAsync();
            var @object = JsonSerializer.Deserialize<Joke[]>(result);
            if (@object is not null)
            {
                joke = @object[0].setup + " " + @object[0].punchline;
            }
        }

        return joke;
    }

    private struct Joke
    {
        public string? setup { get; set; }
        public string? punchline { get; set; }
    }
}