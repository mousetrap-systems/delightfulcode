using System;

/// <summary>
/// Simple mechanism for flagging the author of a class or method,
/// allows keeping track of contributors, useful in a team environment.
/// Similar to NUNIT https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/attributes
/// </summary>
[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
[Author("GPT-4", 2023)]
[Author("Warren James", 2022-2023)]
[Health(CodeStability.Stable)]
public sealed class AuthorAttribute : Attribute
{
    private string name;
    private int[] years;

    /// <summary>
    /// Indicates the (currently known) health of this portion of code.
    /// You can specify multiple years, and also a range, for each year of contribution.
    /// </summary>
    /// <example>
    /// this: [Author("Warren James", 2018-2021, 2023)]
    /// </example>
    /// <param name="contributor_name">Who added to/was responsible for, this code logic? To protect privacy, do not use email addresses.</param>
    /// <param name="years">Optionally indicate the years of contribution (helps identify old code quickly)</param>
    public AuthorAttribute(string contributor_name, params int[] years)
    {
        this.name = contributor_name;
        this.years = years;
    }

    public string Name
    {
        get { return name; }
    }

    public int[] Years
    {
        get { return years; }
    }

}