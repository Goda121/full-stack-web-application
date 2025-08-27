using Microsoft.AspNetCore.Mvc;
using ApiServer.Models;
using System.Collections.Generic;

namespace ApiServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProjectController : ControllerBase
{
    private static List<UserProject> _projects = new List<UserProject>();
    private static int _nextId = 1;
    private readonly ILogger<UserProjectController> _logger;

    public UserProjectController(ILogger<UserProjectController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<UserProject> Get()
    {
        _logger.LogInformation("User projects requested");
        return _projects;
    }

    [HttpGet("{id}")]
    public ActionResult<UserProject> Get(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }
        return project;
    }

    [HttpPost]
    public ActionResult<UserProject> Post(UserProject project)
    {
        _logger.LogInformation("New user project added");
        project.Id = _nextId++;
        _projects.Add(project);
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, UserProject project)
    {
        var existingProject = _projects.FirstOrDefault(p => p.Id == id);
        if (existingProject == null)
        {
            return NotFound();
        }

        existingProject.Email = project.Email;
        existingProject.Password = project.Password;
        existingProject.Age = project.Age;
        existingProject.ProjectName = project.ProjectName;
        existingProject.IsCompleted = project.IsCompleted;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        if (project == null)
        {
            return NotFound();
        }

        _projects.Remove(project);
        return NoContent();
    }
}