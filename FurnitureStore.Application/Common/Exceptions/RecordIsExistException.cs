namespace FurnitureStore.Application.Common.Exceptions;

public class RecordIsExistException : Exception
{
    public RecordIsExistException(string name)
        : base($"Record with name '{name}' already exist")
    {

    }
}
