using System.Linq.Expressions;
using JobRecruitment.Core.Entities.Common;
using JobRecruitment.Core.Repositories;
using JobRecruitment.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace JobRecruitment.DAL.Repositories;

public class GenericRepository<T>(JobRecruitmentDbContext _context):IGenericRepository<T> where T : BaseEntity, new()
{
    protected DbSet<T> Table => _context.Set<T>(); 
    
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
       Table.Remove(entity);
    }

    public async Task DeleteAndSaveAsync(int id)
    {
        await Table.Where(x => x.Id == id).ExecuteDeleteAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        Table.Remove(entity!);
    }

    public async Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, U>> select, bool getAll = false, bool asNoTrack = true, bool isDeleted = true)
    {
       IQueryable<T> query = Table;
       if (!getAll)
       {
           query = query.Where(x => x.IsDeleted != isDeleted);
       }
       if (asNoTrack)
       {
           query = query.AsNoTracking();
       }
       return await query.Select(select).ToListAsync();
    }

    public async Task<IEnumerable<U>> GetAllAsync<U>(Expression<Func<T, U>> select, bool isDeleted = true)
    {
        IQueryable<T> query = Table.Where(x => x.IsDeleted != isDeleted);
        return await query.Select(select).ToListAsync();
    }

    public async Task<U?> GetByIdAsync<U>(int id, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = true)
    {
        IQueryable<T> query = Table.Where(x=>x.Id == id && x.IsDeleted != isDeleted);
        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).FirstOrDefaultAsync();
    }

    public async Task<U?> GetFirstAsync<U>(Expression<Func<T, bool>> expression, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = true)
    {
        IQueryable<T> query = Table.Where(expression).Where(x => x.IsDeleted != isDeleted);
        if(asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<U>> GetWhereAsync<U>(Expression<Func<T, bool>> expression, Expression<Func<T, U>> select, bool asNoTrack = true, bool isDeleted = true)
    {
        IQueryable<T> query = Table.Where(expression).Where(x=>x.IsDeleted != isDeleted);
        if (asNoTrack)
        {
            query = query.AsNoTracking();
        }
        return await query.Select(select).ToListAsync();
    }

    public async Task<bool> IsExistAsync(int id) 
        => await Table.AnyAsync(x => x.Id == id);
    

    public async Task<bool> IsExistAsync(Expression<Func<T, bool>> expression)
        => await Table.AnyAsync(expression);
    
    public void ReverseSoftDelete(T entity)
    {
        entity.IsDeleted = false;
    }

    public async Task ReverseSoftDeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        entity!.IsDeleted = false;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();

    public void SoftDelete(T entity)
    {
        entity.IsDeleted = true;
    }

    public async Task SoftDeleteAsync(int id)
    {
        var entity =  await Table.FindAsync(id);
        entity!.IsDeleted = true;
    }
}