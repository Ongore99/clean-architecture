using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common.Extensions;
using Domain.Common.Helpers;
using Domain.Common.Interfaces;

namespace Domain.Common.Bases;

public class EnumTable<TEnum>: IIdHas<int>
    where TEnum : struct
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    
    [Required, MaxLength(128)]
    public string Name { get; set; }
    
    [MaxLength(128)]
    public string DisplayName { get; set; }
    
    protected EnumTable() { }

    public EnumTable(TEnum enumType)
    {
        ExceptionHelpers.ThrowIfNotEnum<TEnum>();

        DisplayName = enumType.GetDisplayName();
        Id = (int)(object) enumType;
        Name = enumType.ToString()!;
    }
    
    public static implicit operator EnumTable<TEnum>(TEnum enumType) => new EnumTable<TEnum>(enumType);
    public static implicit operator TEnum(EnumTable<TEnum> status) => (TEnum)(object) status.Id;
}