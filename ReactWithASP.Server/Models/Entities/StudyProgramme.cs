using System.ComponentModel.DataAnnotations;

namespace ReactWithASP.Server.Models.Entities
{
    public class StudyProgramme(string title, string description, int duration) : Entity<int>
    {
        [MaxLength(100)] public string Title { get; private set; } = title;
        [MaxLength(500)] public string Description { get; private set; } = description;
        public int Duration { get; private set; } = duration;

        public void SetValues(string title, string description, int duration)
        => (Title, Description, Duration) = (title, description, duration);

        public List<Subject> Subjects { get; } = new();
    }

}
