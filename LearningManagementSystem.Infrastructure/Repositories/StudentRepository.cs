using LearningManagementSystem.Application.Interfaces;
using LearningManagementSystem.Domain.Entities;
using LearningManagementSystem.Infrastructure.Data;

namespace LearningManagementSystem.Infrastructure.Repositories;

public class StudentRepository(IDbContext context)
    : BaseRepository<Student>(context.Students), IStudentRepository;
