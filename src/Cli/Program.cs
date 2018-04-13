using System;
using System.Threading.Tasks;
using System.Text;
using Refit;

namespace Cli
{
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

  public interface IGitHubApi
  {
    [Get("/user")]
    [Headers("Authorization: token")]
    Task<User> GetUser();
  }


  class Program
  {
    static async Task Main(string[] args)
    {

      var token = "958cae560dc43851fe43b41d7eea9dcdcc7999e9";
      var settings = new RefitSettings() {
        AuthorizationHeaderValueGetter = () => Task.FromResult(token)
      };
      var api = RestService.For<IGitHubApi>("https://api.github.com", settings);
      try
      {
        var user =  await api.GetUser();
        Console.WriteLine(user.Login);
      }
      catch (ApiException ex)
      {
        Console.WriteLine(ex.Message);
      }
    }
  }
}
