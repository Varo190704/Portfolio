using Domain.Aplication.Interfaces;
using Domain.Infrastructure.SqlConecc;
using Domain.Utilities;
using Domain.Validators;

namespace Domain.Aplication.Services;

public class ResourceManager : IResourceManager
{
    private readonly ResourceInSQL ResourceRepo;

    public ResourceManager(ResourceInSQL resourceRepo)
    {
        ResourceRepo = resourceRepo;
    }
    
    public IEnumerable<Resource> getAllResources()
    {
        return ResourceRepo.GetAll();
    }

    public Resource getResourcesByName(string name)
    {
        return ResourceRepo.getByName(name);
    }

    public void createResource(Resource resource)
    {
        if (Equals(resource, null))
        {
            throw new ArgumentException("Invalid Resource");
        }
        if (ResourceRepo.getByName(resource.Name) != null)
        {
            throw new ArgumentException("Resource already exists");
        }
        ResourceRepo.Add(resource);
    }

    public void deleteResource(Resource resource)
    {
        if (Equals(resource, null))
        {
            throw new ArgumentException("Invalid Resource");
        }

        if (ResourceRepo.getByName(resource.Name) == null)
        {
            throw new ArgumentException("Resource does not exists");
        }
        ResourceRepo.Remove(resource);
    }

    public void updateResource(Resource resource)
    {
        if (ResourceRepo.getByName(resource.Name) == null)
        {
            throw new ArgumentException("Resource does not exists");
        }
        ResourceRepo.Update(resource);
    }

    public bool isUsed(Resource resource, DateOnly startDate, DateOnly endDate)
    {
        return ResourceRepo.isUsed(resource, startDate, endDate);;
    }

    public ResourceUsed fUsed(Resource resource)
    {
        return ResourceRepo.getFUsed(resource);
    }
    
    public ResourceUsed getUsed(Resource resource, DateOnly startDate, DateOnly endDate)
    {
        return ResourceRepo.getUsedR(resource, startDate, endDate);;
    }

    public void assigned(string resourceName, string taskName, string selProjectName, DateOnly assign2StartDate,
        DateOnly assign2EndDate)
    {
        ResourceRepo.assigned(resourceName, taskName, selProjectName, assign2StartDate, assign2EndDate);
    }

    public void delResourceUsed(string resourceName, string project, string task)
    {
        ResourceRepo.RemoveUsagesR(resourceName, project, task);
    }
}