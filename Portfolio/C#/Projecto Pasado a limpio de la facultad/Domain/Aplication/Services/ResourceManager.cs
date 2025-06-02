using Domain.Aplication.Interfaces;
using Domain.Utilities;
using IList = Domain.Infrastructure.Persistence;

namespace Domain.Aplication.Services;

public class ResourceManager : IResourceManager
{
    private readonly IList.IList<Resource> ResourceRepo;

    public ResourceManager(IList.IList<Resource> resourceRepo)
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
}