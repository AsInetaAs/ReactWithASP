using Microsoft.EntityFrameworkCore;
using ReactWithASP.Server.Data;
using ReactWithASP.Server.Models.DTOs;
using ReactWithASP.Server.Models.Entities;

namespace ReactWithASP.Server.Services;

public class GetLecturerService(AppDbContext context) : IGetLecturerService
    {
        public async Task<List<LecturerDto>> GetAll()
        {
            var lecturers = await context.Lecturers.ToListAsync();
            List<LecturerDto> results = new();

            foreach (var lecturer in lecturers)
            {
                results.Add(MapDto(lecturer));
            }
            return results;
        }
        public async Task<LecturerDto?> Get(int id)
        {
            var lecturer = await context.Lecturers.FirstOrDefaultAsync(i => i.Id == id);
            if (lecturer == null) return null;
            return MapDto(lecturer);
        }

        private LecturerDto MapDto(Lecturer lecturer) =>
            new LecturerDto(lecturer.Id, lecturer.FirstName, lecturer.LastName, lecturer.Email);
    }

