using Domain.Utilities;

namespace Domain.Aplication.Interfaces;

public interface IResourceManager
{
    IEnumerable<Resource> getAllResources();
    Resource getResourcesByName(String name);
    void createResource(Resource resource);
    void deleteResource(Resource resource);
    void updateResource(Resource resource);
}