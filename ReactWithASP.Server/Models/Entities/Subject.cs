using System.ComponentModel.DataAnnotations;

namespace ReactWithASP.Server.Models.Entities
{
    public class Subject(string title, int credits) : Entity<int>
    {
        [MaxLength(100)] public string Title { get; private set; } = title;
        public int Credits { get; private set; } = credits;

        public void SetValues(string title, int credits)
        => (Title, Credits) = (title, credits);
    }

}