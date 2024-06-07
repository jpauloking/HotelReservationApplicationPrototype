
namespace HotelReservationDesktop.Commands;

public abstract class AsyncCommandBase : CommandBase
{
    private bool isExecuting;

    public bool IsExecuting
    {
        get { return isExecuting; }
        private set
        {
            isExecuting = value;
            RaiseCanExecuteChanged();
        }
    }

    public override bool CanExecute(object? parameter)
    {
        return !IsExecuting && base.CanExecute(parameter);
    }

    public override async void Execute(object? parameter)
    {
        IsExecuting = true;
        try
        {
            await ExecuteAsync(parameter);
        }
        finally
        {
            IsExecuting = false;
        }
    }

    public abstract Task ExecuteAsync(object? parameter);
}
