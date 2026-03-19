using System.ComponentModel;
using Umbraco.Cms.Core.Services;

namespace Protenacity.Cake.Web.Core.Property;

public enum EditorCodeLanguage
{
    [Description("C++")]
    Cpp,

    [Description("C#")]
    CSharp,

    [Description("Javascript")]
    Javascript,

    [Description("Css")]
    Css,

    [Description("Command Line")]
    CommandLine,

    [Description("Dockerfile")]
    Dockerfile,

    [Description("Html")]
    Html,

    [Description("Json")]
    Json,

    [Description("Markdown")]
    Markdown,

    [Description("Python")]
    Python,

    [Description("Razor")]
    Razor,

    [Description("Scss")]
    Scss,

    [Description("Sql")]
    Sql,

    [Description("Typescript")]
    Typescript
}

public class EditorCodeLanguageValueConverter(IDataTypeService dataTypeService)
    : PropertyValueConverterBase<EditorCodeLanguage>(dataTypeService)
{
    public override string DataTypeName => "Editor Code Language";
}
