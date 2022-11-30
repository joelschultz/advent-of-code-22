using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

public static class ProblemInputFetcher {
    static string s_root = "../../../Temp/";

    public static async Task<string> Fetch(int year, int day) {
        PrepareRootDir();

        Console.WriteLine($"Fetching input for year {year}, day {day}.");

        string cached = LoadFromCache(year, day);
        if(!string.IsNullOrEmpty(cached)) {
            Console.WriteLine($"Found cached.");
            return cached;
        }

        string received = await LoadFromServer(year, day);
        if(!string.IsNullOrEmpty(received)) {
            SaveToCache(year, day, received);
        }

        return received;
    }

    private static async Task<string> LoadFromServer(int year, int day) {
        for(int i = 0; i < 2; i++) {
            try {
                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("Cookie", "session=" + GetSessionToken());

                HttpResponseMessage response = await client.GetAsync(new Uri($"http://adventofcode.com/{year}/day/{day}/input"));

                if(response.StatusCode == HttpStatusCode.OK) {
                    string received = await response.Content.ReadAsStringAsync();
                    received = received.TrimEnd('\n');

                    Console.WriteLine($"Received.");
                    return received;
                }
            }
            catch(Exception e) {
                Console.WriteLine("Exception when fetching problem input: " + e.Message);
            }

            Console.WriteLine("Something went wrong! Maybe your session token has expired!");
            Console.Write("Enter a new one: ");
            string? sessionToken = Console.ReadLine();
            await File.WriteAllTextAsync(s_root + "session.txt", sessionToken);
        }

        return string.Empty;
    }

    static string GetSessionToken() {
        try {
            return File.ReadAllText(s_root + "session.txt");
        }
        catch(Exception) {
            return string.Empty;
        }
    }

    static string LoadFromCache(int year, int day) {
        try {
            return File.ReadAllText(GetInputFilename(year, day));
        }
        catch(Exception) {
            return string.Empty;
        }
    }

    static void SaveToCache(int year, int day, string data) {
        File.WriteAllText(GetInputFilename(year, day), data);
    }

    static string GetInputFilename(int year, int day) {
        return s_root + year + "-" + day + ".txt";
    }

    static void PrepareRootDir() {
        if(!Directory.Exists(s_root)) {
            Directory.CreateDirectory(s_root);
        }
    }
}