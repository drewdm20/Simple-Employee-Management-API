using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController: ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService service)
    {
        _employeeService = service;
    }

    [HttpGet]
    public async Task<List<Employee>> GetEmployees() => await _employeeService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployee(string id)
    {
        var employee = await _employeeService.GetAsync(id);

        if (employee is null)
        {
            return NotFound();
        }

        return employee;
    }

    [HttpPost]
    public async Task<IActionResult> PostAddEmployee(Employee employee)
    {
        await _employeeService.CreateAsync(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = employee.empID }, employee);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(string id, Employee employee)
    {
        var emp = await _employeeService.GetAsync(id);

        if (emp is null)
        {
            return NotFound();
        }

        employee.empID = emp.empID;
        await _employeeService.UpdateAsync(id, employee);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        var employee = await _employeeService.GetAsync(id);
        if (employee is null)
        {
            return NoContent();
        }

        await _employeeService.DeleteAsync(id);
        return Ok();
    }
}