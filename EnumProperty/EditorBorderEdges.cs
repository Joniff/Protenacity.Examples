using System.ComponentModel;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Examples.EnumProperty;

[Flags]
public enum EditorBorderEdges
{
    [Description("Top")]
    Top = 1,

    [Description("Bottom")]
    Bottom = 2,

    [Description("Left")]
    Left = 4,

    [Description("Right")]
    Right = 8,

    // Not set in Umbraco
    [Description("None")]
    None = 0,

    // Not set in Umbraco
    [Description("All")]
    All = EditorBorderEdges.Top | EditorBorderEdges.Bottom | EditorBorderEdges.Left | EditorBorderEdges.Right
}

public class EditorBorderEdgesValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorBorderEdges>(dataTypeService)
{
    public override string DataTypeName => "Editor Border Edges";
}