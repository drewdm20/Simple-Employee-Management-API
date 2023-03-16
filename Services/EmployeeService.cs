using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApplication3.Models;

namespace WebApplication3.Services;

public class EmployeeService
{
    private readonly IMongoCollection<Employee> _employeesCollection;

    public EmployeeService(IOptions<EmployeeDatabaseSettings> employeeDatabaseSettings)
    {
        var client = new MongoClient(employeeDatabaseSettings.Value.ConnectionString);
        var database = client.GetDatabase(employeeDatabaseSettings.Value.DatabaseName);
        _employeesCollection = database.GetCollection<Employee>(employeeDatabaseSettings.Value.EmployeeCollectionName);
    }

    public async Task<List<Employee>> GetAsync() => await _employeesCollection.Find(_ => true).ToListAsync();

    public async Task<Employee?> GetAsync(string id) =>
        await _employeesCollection.Find(x => x.empID == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Employee newEmployee) => await _employeesCollection.InsertOneAsync(newEmployee);
    
    public async Task UpdateAsync(string id, Employee newEmployee) => await _employeesCollection.ReplaceOneAsync(x => x.empID == id, newEmployee);
    
    public async Task DeleteAsync(string id) => await _employeesCollection.DeleteOneAsync(x => x.empID == id);
}