using NotissimusTest.Models;

namespace NotissimusTest.Services;

public interface ICategoryService
{
    Task<Category?> GetByIdAsync(int id);
}