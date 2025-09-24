using System.ComponentModel.DataAnnotations;

namespace ReactWithASP.Server.Models.Entities
{
    public class Group(string title, int year) : Entity<int>
    {
        [MaxLength(20)] public string Title { get; private set; } = title;
        public int Year { get; private set; } = year;
        public void SetValues(string title, int year)
        => (Title, Year) = (title, year);
    }
}





