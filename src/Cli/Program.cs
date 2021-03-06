﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Refit;

namespace Cli
{
  public class GhConfig
  {
    public string PAT { get; set; }
  }

  public class User
  {
    public string Login { get; set; }
    public int Id { get; set; }
    public string AvatarUrl { get; set; }
    public string GravatarId { get; set; }
    public string Url { get; set; }
    public string HtmlUrl { get; set; }
    public string FollowersUrl { get; set; }
    public string FollowingUrl { get; set; }
    public string GistsUrl { get; set; }
    public string StarredUrl { get; set; }
    public string SubscriptionsUrl { get; set; }
    public string OrganizationsUrl { get; set; }
    public string ReposUrl { get; set; }
    public string EventsUrl { get; set; }
    public string ReceivedEventsUrl { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Company { get; set; }
    public string Blog { get; set; }
    public string Location { get; set; }
    public string Email { get; set; }
    public bool? Hireable { get; set; }
    public string Bio { get; set; }
    public int PublicRepos { get; set; }
    public int Followers { get; set; }
    public int Following { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public int PublicGists { get; set; }
  }

  [Headers("User-Agent: Github CLI (gh)")]
  public interface IGitHubApi
  {
    [Get("/user")]
    Task<User> GetUser([Header("Authorization")] string authorization);
  }


  class Program
  {
    static async Task Main(string[] args)
    {
      var configPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}/.config/gh.json";

      if (!File.Exists(configPath)) {
        Console.WriteLine("No config file found at {0}", configPath);
        return;
      }

      var configFile = File.ReadAllText(configPath);
      GhConfig config = JsonConvert.DeserializeObject<GhConfig>(configFile);
      var auth = $"token {config.PAT}";

      var api = RestService.For<IGitHubApi>("https://api.github.com");
      try
      {
        var user =  await api.GetUser(auth);
        Console.WriteLine(user.Login);
      }
      catch (ApiException ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
