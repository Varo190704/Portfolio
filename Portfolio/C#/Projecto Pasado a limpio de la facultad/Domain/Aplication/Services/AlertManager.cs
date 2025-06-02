using Domain.Aplication.Interfaces;
using Domain.Utilities;
using IList = Domain.Infrastructure.Persistence;

namespace Domain.Aplication.Services;

public class AlertManager : IAlertManager
{
    private readonly IList.AlertRepositoryInMemory AlertRepo;

    public AlertManager(IList.AlertRepositoryInMemory AlertRepo)
    {
        this.AlertRepo = AlertRepo;
    }
    
    public IEnumerable<Alert> getAllAlerts()
    {
        return AlertRepo.GetAll();
    }

    public Alert getAlertsById(int id)
    {
        return AlertRepo.getById(id);
    }

    public void createAlert(Alert alert)
    {
        AlertRepo.Add(alert);
    }

    public void deleteAlert(Alert alert)
    {
        if (alert == null)
        {
            throw new ArgumentNullException("Invalid Alert");
        }

        if (AlertRepo.getById(alert.Id) == null)
        {
            throw new ArgumentException("Invalid Alert");
        }
        AlertRepo.Remove(alert);
    }

    public void updateAlert(Alert alert)
    {
        if (AlertRepo.getById(alert.Id) == null)
        {
            throw new ArgumentException("Resource does not exists");
        }
        AlertRepo.Update(alert);
    }
}