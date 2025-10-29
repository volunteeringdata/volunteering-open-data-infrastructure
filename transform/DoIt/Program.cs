using DoIt.Models;
using System.Text.Json;

var activities = await JsonSerializer.DeserializeAsync<IEnumerable<Activity>>(File.OpenRead("../../../../../data/doit/activities.json"));
Console.WriteLine(activities);
