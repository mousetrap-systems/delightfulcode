﻿/// <summary>
/// Internal flags used in attributes, allows internal classification of the codebase.
/// </summary>
[Author("Warren James", 2022)]
[Health(CodeStability.Stable)]
public enum CodeStability
{
    /// <summary>
    /// Sometimes a chunk of code doesn't have anything specifically wrong with it (i.e. no specific issues) and works, but it stinks a bit.
    /// If your 'code hound' nose detects a whiff then use this marker so that someone can come back to it later (when time permits)
    /// Be careful with using this decoration because it HAS POTENTIAL TO BE interpreted as offensive.
    /// At least include summary as to what you are smelling, so that the assessment can end up being be objective rather than subjective.
    /// </summary>
    CodeSmells,

    /// <summary>
    /// This flag is the equivalent to an 'under construction' - generally for new functions or code but may also be used to highlight bad stuff.
    /// Indicates to other developers the code is not to be referenced until otherwise cleared for general csonsumption.
    /// NOT THE SAME AS 'OBSOLETE'
    /// </summary>
    DontUseThis,

    /// <summary>
    /// Needs markup with XML comments, do it when we get around to it.
    /// </summary>
    RequiresCommentary,

    /// <summary>
    /// This code has the potential to change at any time and may indicate business requirements are still being decided.
    /// Use this flag for low-impact code blocks which you're happy for others to come back to and rework later.
    /// </summary>
    Experimental,

    /// <summary>
    /// If this code block is KNOWN to have issues, use this as a warning flag to other developers so that they know.
    /// The goal is to allow 'Issues' code to be easily identified and targeted, and should be updated after those issues are resolved.
    /// </summary>
    Issues,

    /// <summary>
    /// Marks code known which may be easy to shatter. Be careful with changing this class since it might break due to various dependencies.
    /// This code would be a good candidate to refactor in the future, but requires careful handling.
    /// </summary>
    FragileHandleWithCare,

    /// <summary>
    /// Code has unknown authors, unknown time pressures, unknown usages and unknown dependencies - yet "still being used".
    /// Our usage is simply to flag code which is known to be part of an 'older generation' of codebase.
    /// This is a SUBJECTIVE flag, since technically "all code is legacy code as soon as it's written"
    /// ref: https://stackoverflow.com/questions/4174867/what-is-the-definition-of-legacy-code
    /// </summary>
    Legacy,

    /// <summary>
    /// SPECIAL: Can be used to flag an item for possible future deletion. Assumes a desire already exists to get rid of this code,
    /// but cannot currently be done due to time constraints or other dependencies.
    /// Regular usage gives us a great immediate map for code cleanup.
    /// </summary>
    PleaseRemoveThisOneDay,

    /// <summary>
    /// Indicates the code block may have been seen in similar form or is similar to some other function/sequence and could be merged.
    /// Gives a fighting chance to whoever is maintaining the code to do a quick search and find out if any duplicates actually currently still exist.
    /// </summary>
    PossiblyContainsDuplicateCode,

    /// <summary>
    /// Use this to flag a bit of code which you think is a nice target for optimization - e.g. you know it can be done better, or just suspect it could be compacted.
    /// It might be something you've already considered but just don't have the time to review now (or wish to come back to). Leave a good comment trail to assist the potential reviewer - after all you are currently the closest person to the code at this point.
    /// </summary>
    RequiresReview,

    /// <summary>
    /// Indicates the code has no major issues and decent levels of readability, and good naming patterns.
    /// Only change this code unless you can actually really introduce some high-level improvements.
    /// </summary>
    Stable,

    /// <summary>
    /// Mature and time-tested code which we don't expect to be changed in the near future (and it's compact, well-written, tested, reviewed, used everywhere, etc)
    /// Use of this flag indicates 'please refrain from making any specific edits or extensions unless otherwise required'.
    /// In theory this will be used mostly in common modules where it becomes possible, practical and beneficial to refine code to this state.
    /// </summary>
    SuperStable,

    /// <summary>
    /// Marks the code as being a good candidate for Optimization - meaning improving this would produce significant benefits and should be attended to next.
    /// Can (and should) be removed once the code block is actually reworked. Using this flag provides us an overall map for the best optimization points.
    /// </summary>
    OptimizationCandidate,

    /// <summary>
    /// Code in this section may be part of a larger effort to relocate or consolidate objects and currently has no specific classification.
    /// Look for clues and comments about ownership and versioning before you start extending stuff.
    /// </summary>
    TemporaryMigration,

    /// <summary>
    /// Default, but can be used to indicate that an assessment is pending.
    /// </summary>
    Unknown
}