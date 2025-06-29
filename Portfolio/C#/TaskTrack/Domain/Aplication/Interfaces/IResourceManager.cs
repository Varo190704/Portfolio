using Domain.Utilities;
using Domain.Validators;

namespace Domain.Aplication.Interfaces;

public interface IResourceManager
{
    IEnumerable<Resource> getAllResources();
    Resource getResourcesByName(String name);
    void createResource(Resource resource);
    void deleteResource(Resource resource);
    void updateResource(Resource resource);
    bool isUsed(Resource resource, DateOnly startDate, DateOnly endDate);
    ResourceUsed getUsed(Resource resource, DateOnly startDate, DateOnly endDate);
    void assigned(string resourceName, string taskName, string selProjectName, DateOnly assign2StartDate, DateOnly assign2EndDate);
    ResourceUsed fUsed(Resource resource);
    void delResourceUsed(string resourceName, string projectName, string taskName);
}