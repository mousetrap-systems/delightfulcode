using System;

/// <summary>
/// Simple mechanism for flagging the author of a class or method,
/// allows keeping track of contributors, useful in a team environment.
/// Similar to NUNIT https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/attributes
/// </summary>
[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
[Author("Warren James", 2022)]
[Health(CodeStability.Stable)]
public sealed class AuthorAttribute : Attribute
{
    private string name;

    /// <summary>
    /// Indicates the (currently known) health of this portion of code.
    /// </summary>
    /// <param name="contributor_name">Who added to/was responsible for, this code logic?</param>
    /// <param name="year">Optionally indicate the year written (helps identify old code quickly)</param>
    public AuthorAttribute(string contributor_name, int year = 0)
    {
        this.name = contributor_name;
    }

    public string Name
    {
        get { return name; }
    }

}