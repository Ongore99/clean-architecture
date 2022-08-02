namespace Domain.Common.Interfaces;

public interface IIdHas<T>
{
    public T Id { get; set; }
}