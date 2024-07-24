namespace Ordering.Domain.Exeptions;

public class DomainExeption : Exception
{
    public DomainExeption(string message) : base($"Domain Exception: \"{message}\" throws from Domain Layer.")
    {
    }
}
