using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

namespace ReactWithASP.Server.Services;

    public class SaveLecturerService(AppDbContext context) : ISaveLecturerService
    {
        public async Task Store(LecturerDto dto)
        {
            var lecturer = new Lecturer(dto.FirstName, dto.LastName, dto.Email);
            context.Lecturers.Add(lecturer);
            await context.SaveChangesAsync();
        }

        public async Task Update(int id, LecturerDto dto)
        {
            var lecturer = await context.Lecturers.FirstOrDefaultAsync(i => i.Id == id);
            if (lecturer != null)
            {
                lecturer.SetValues(dto.FirstName, dto.LastName, dto.Email);
                context.Lecturers.Update(lecturer);
                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var lecturer = await context.Lecturers.FirstOrDefaultAsync(i => i.Id == id);
            if (lecturer != null)
            {
                context.Lecturers.Remove(lecturer);
                await context.SaveChangesAsync();
            }
    }
}

