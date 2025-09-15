namespace WeChooz.TechAssessment.Application;

public static class StringExtensions
{
    public static string CamelCase(this string str) => System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(str);
}