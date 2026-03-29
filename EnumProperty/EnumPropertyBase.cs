using Protenacity.Examples.Extensions;
using System.Text.Json;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Examples.EnumProperty;

public abstract class PropertyValueConverterBase<E>(IDataTypeService dataTypeService) : IPropertyValueConverter where E : Enum
{
    public abstract string DataTypeName { get; }

    private IEnumerable<string>? GetConfigurationItems(string editorAlias, object? config)
    {
        switch (editorAlias)
        {
            case Constants.PropertyEditors.Aliases.DropDownListFlexible:
                return (config as DropDownFlexibleConfiguration)?.Items;

            default:
                return (config as ValueListConfiguration)?.Items;
        }
    }

    public bool IsConverter(IPublishedPropertyType propertyType)
    {
        if (!propertyType.IsUserProperty)
        {
            return false;
        }

        var dataType = dataTypeService.GetByEditorUiAlias(propertyType.DataType.EditorUiAlias).Result.FirstOrDefault(d => d.Id == propertyType.DataType.Id);

        if (dataType?.Name != DataTypeName)
        {
            return false;
        }

        var items = GetConfigurationItems(propertyType.DataType.EditorAlias, dataType.ConfigurationObject);
        if (items?.Any() != true)
        {
            return false;
        }

        foreach (var item in items)
        {
            if (EnumExtensions.ParseByDescription<E>(item, true, default(E)) == null)
            {
                throw new ArgumentOutOfRangeException("Value " + item + " is not a valid value for " + DataTypeName + " Data Type, because it doesn\'t exist in the corresponding " + typeof(E).Name + " Enum");
            }
        }
        return true;
    }

    private object? Convert(object? obj)
    {
        if (obj == null)
        {
            return null;
        }

        switch (obj)
        {
            case string source:
                if (string.IsNullOrEmpty(source))
                {
                    return null;
                }
                if (source[0] == '[' && source[source.Length - 1] == ']')
                {
                    return EnumExtensions.ParseByDescription<E>(JsonSerializer.Deserialize<IEnumerable<string>>(source), true, default(E));
                }
                return EnumExtensions.ParseByDescription<E>(source, true, default(E));

            case E esource:
                return esource;

            default:
                throw new ArgumentOutOfRangeException("Unknown type");
        }
    }


    public object? ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object? inter, bool preview)
        => Convert(inter );

    public object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview)
    {
        throw new NotImplementedException();
    }

    public object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview)
        => Convert(source);

    public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) => PropertyCacheLevel.Element;
    public bool? IsValue(object? value, PropertyValueLevel level) => true;
    public Type GetPropertyValueType(IPublishedPropertyType propertyType) =>
        typeof(Nullable<>).MakeGenericType(typeof(E));
}