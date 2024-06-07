using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HotelReservationDesktop.ViewModels;

public class ValidatableViewModelBase : INotifyDataErrorInfo
{
    public Dictionary<string, List<string>> Errors { get; set; } = [];
    public bool HasErrors => Errors.Count > 0;
    public bool Isvalid => Validator.TryValidateObject(this, new ValidationContext(this), null);

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        return Errors.GetValueOrDefault(propertyName!, []);
    }

    public void OnErrorsChanged(string? propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public void Validate(object value, string propertyName)
    {
        List<ValidationResult> validationResults = [];
        Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults);
        bool hasValidationErrors = validationResults.Count > 0;
        if (hasValidationErrors)
        {
            AddErrors(propertyName, validationResults.Select(result => result.ErrorMessage!).ToList());
        }
        else
        {
            RemoveErrors(propertyName);
        }
    }

    private void RemoveErrors(string propertyName)
    {
        Errors.Remove(propertyName);
    }

    private void AddErrors(string propertyName, List<string> propertyErrors)
    {
        RemoveErrors(propertyName);
        Errors.Add(propertyName, propertyErrors);
    }
}
