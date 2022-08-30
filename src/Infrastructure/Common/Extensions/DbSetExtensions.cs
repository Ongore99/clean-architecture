using Domain.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common.Extensions;
    
public static class DbSetExtensions
{
        public static async Task AddOrUpdateEnumValues<T, TEnum>(this DbSet<T> dbSet, Func<TEnum, T> converter)
            where T : class, IIdHas<int>
        {
            var enums = Enum.GetValues(typeof(TEnum))
                .Cast<object>()
                .Select(value => converter((TEnum) value))
                .ToList();
            
            foreach (var e in enums)
            {
                var foundEnum = await dbSet
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == e.Id);
                
                if (foundEnum == null)
                {
                    await dbSet.AddAsync(e);
                }
                else
                {
                    dbSet.Update(e);
                }
            }
        }  
}