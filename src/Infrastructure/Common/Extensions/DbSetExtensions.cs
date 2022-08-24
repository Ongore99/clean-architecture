using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Extensions;

public static class DbSetExtensions
{
    public static void AddOrUpdateEnumValues<T, TEnum>(this DbSet<T> dbSet, Func<TEnum, T> converter)
        where T : class, IIdHas<int>
    {
        var enums = Enum.GetValues(typeof(TEnum))
            .Cast<object>()
            .Select(value => converter((TEnum) value))
            .ToList();
        
        foreach (var e in enums)
        {
            var foundEnum = dbSet
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == e.Id);
            
            if (foundEnum == null)
            {
                dbSet.Add(e);
            }
            else
            {
                dbSet.Update(e);
            }
        }
    }  
}