using ENTITY1.Data;
using ENTITY1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENTITY1.Services;

public class StudentService : IStudentService
{
    private readonly MyDbContext _context;
    public StudentService(MyDbContext context)
    {
        _context = context;
    }
    public async Task<IList<Student>> GetAllAsync()
    {
        return _context.Students != null ? await _context.Students.ToListAsync() : new List<Student>();

    }

    public async Task<Student?> GetOneAsync(int id)
    {
        if (_context.Students == null) return null;
        
        var queryable = _context.Students.AsQueryable();
        queryable = queryable.Where(x => x.Id == id);
        var sql = queryable.ToQueryString(); 

        return await queryable.SingleOrDefaultAsync();
    }
    public async Task<Student?> AddAsync(Student entity)
    {
        if (_context.Students == null) return null;

        await _context.Students.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Student?> EditAsync(Student entity)
    {
        if (_context.Students == null) return null;
        
        _context.Students.Update(entity);
        await _context.SaveChangesAsync();

        return entity;

    }

    public async Task RemoveAsync(int id)
    {
        if (_context.Students == null) return;
        var entity = await _context.Students.SingleOrDefaultAsync(x => x.Id == id);
        if (entity == null) return;
        _context.Students.Remove(entity);
        await _context.SaveChangesAsync();
        

    }
}