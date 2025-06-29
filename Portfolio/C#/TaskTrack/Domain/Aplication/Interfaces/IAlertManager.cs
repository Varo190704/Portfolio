using Domain.Utilities;

namespace Domain.Aplication.Interfaces;

public interface IAlertManager
{
    IEnumerable<Alert> getAllAlerts();
    Alert getAlertsById(int id);
    void createAlert(Alert alert);
    void deleteAlert(Alert alert);
    void updateAlert(Alert alert);
}