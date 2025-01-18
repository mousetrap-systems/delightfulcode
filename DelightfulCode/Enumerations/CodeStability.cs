namespace DelightfulCode
{
    /// <summary>
    /// Internal flags used in attributes, allows internal classification of the codebase.
    /// Every year I ask GPT-4 to suggest new items, I also add ones that I come across in my own usage.
    /// </summary>
    [Author("GPT-4", 2023, 2024)]
    [Author("Warren James", 2018 - 2021, 2023 - 2025)]
    [Health(CodeStability.Stable)]
    public enum CodeStability
    {
        /// <summary>
        /// Code follows best practices and design patterns and can be used as a reference or example for other implementations.
        /// Use this flag to indicate that the code is exemplary and can serve as a guide for other developers.
        /// </summary>
        BestPractices,

        /// <summary>
        /// Sometimes a chunk of code doesn't have anything specifically wrong with it (i.e. no specific issues) and works, but it stinks a bit.
        /// If your 'code hound' nose detects a whiff then use this marker so that someone can come back to it later (when time permits)
        /// Be careful with using this decoration because it HAS POTENTIAL TO BE interpreted as offensive.
        /// At least include summary as to what you are smelling, so that the assessment can end up being be objective rather than subjective.
        /// </summary>
        CodeSmells,

        /// <summary>
        /// Code is written to comply with a specific standard or regulation.
        /// Use this flag to indicate that changes to the code need to maintain compliance with the specified standard or regulation.
        /// </summary>
        ComplianceRequired,

        /// <summary>
        /// The code has been deprecated, meaning it should no longer be used in new development.
        /// This flag helps identify outdated code that should be replaced by a newer implementation.
        /// SIMILAR to '[Obsolete]' but the term "Deprecated" specifically indicates that a better
        /// alternative exists, and the deprecated code may be removed in future releases.
        /// </summary>
        Deprecated,

        /// <summary>
        /// This flag is the equivalent to an 'under construction' - generally for new functions or code but may also be used to highlight bad stuff.
        /// Indicates to other developers the code is not to be referenced until otherwise cleared for general csonsumption.
        /// NOT THE SAME AS 'OBSOLETE'
        /// </summary>
        DontUseThis,

        /// <summary>
        /// Code is not yet ready for use, but will be in the future.
        /// This may be a stub or empty name.
        /// </summary>
        FutureUsage,

        /// <summary>
        /// Needs markup with XML comments, do it when we get around to it.
        /// </summary>
        RequiresCommentary,

        /// <summary>
        /// Code requires a review from a subject matter expert (SME) or a specific team member.
        /// Use this flag to indicate that specialized knowledge is needed to properly review or assess the code.
        /// </summary>
        RequiresExpertReview,

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
        /// Code has been partially written, but not fully implemented or tested.
        /// This is a placeholder for functionality that needs to be completed.
        /// </summary>
        InProgress,

        /// <summary>
        /// Marks code known which may be easy to shatter even if you change something small.
        /// This could be because of dependencies or really sensitive data containers.
        /// Be careful with changing this class since it might break due to various dependencies.
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
        /// The functionality inside this code is really really very quite important to the business, so be very careful with any changes.
        /// This flag is more to do with BUSINESS procedure, and indicates that the concepts here are buried deep within the organisation.
        /// If this code were to break or be altered in any way, then this would be a major impact / major disruption.
        /// suggest: always use pair programming to review, and additionally should ensure that all functionality is fully tested.
        /// </summary>
        MissionCritical,

        /// <summary>
        /// Indicates that performance is critical for this code.
        /// Optimizations for speed, memory, or responsiveness should be prioritized.
        /// Changes to this code should be reviewed with performance impact in mind.
        /// Examples include real-time systems, high-frequency trading, or latency-sensitive operations.
        /// </summary>
        PerformanceCritical,

        /// <summary>
        /// Similar to 'Deprecated' but a final decision hasn't been made yet.
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
        /// Flags code that is challenging to read or understand, even if functional.
        /// This could be due to poor naming, lack of comments, or overly complex logic.
        /// Refactoring for readability is recommended. Obviously this is subjective,
        /// so whoever adds the flag should be ready to defend their commentary and reasonings.
        /// This is intended primarily to initiate discussion.
        /// </summary>
        ReadabilityConcerns,

        /// <summary>
        /// This code is being actively refactored or redesigned.
        /// Use this flag to indicate that the current code is expected to change
        /// and to notify other developers that they should coordinate any changes with you.
        /// </summary>
        RefactoringInProgress,

        /// <summary>
        /// Use this to flag a bit of code which you think is a nice target for optimization - e.g. you know it can be done better, or just suspect it could be compacted.
        /// It might be something you've already considered but just don't have the time to review now (or wish to come back to). Leave a good comment trail to assist the potential reviewer - after all you are currently the closest person to the code at this point.
        /// </summary>
        RequiresReview,

        /// <summary>
        /// Code requires additional tests or has known test coverage gaps.
        /// Use this flag to indicate that improvements to the test suite are needed.
        /// </summary>
        RequiresMoreTests,

        /// <summary>
        /// Code has known security vulnerabilities or may be susceptible to specific security risks.
        /// Use this flag to indicate that a security review or improvements are needed to ensure the code is secure.
        /// </summary>
        SecurityConcerns,

        /// <summary>
        /// Code has been optimized for a specific use case or platform and may not be suitable for general use.
        /// Use this flag to indicate that changes or adaptations may be necessary for broader applicability.
        /// </summary>
        SpecializedImplementation,

        /// <summary>
        /// Indicates the code has no major issues and decent levels of readability, and good naming patterns.
        /// It is close to it's 'final state' and the internals and API's generally resist entropy.
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
        /// Code is platform-specific and may not work correctly or optimally on other platforms.
        /// Use this flag to indicate that the code is tied to a specific platform and changes for cross-platform compatibility may be necessary.
        /// </summary>
        PlatformSpecific,

        /// <summary>
        /// Code in this section is currently in a location which is not expected to be the FINAL location.
        /// This means it may be part of a larger effort to relocate or consolidate objects and needs a final home to remove the flag.
        /// Useful when you know that the code block has to move but you're unsure yet where that needs to go.
        /// </summary>
        TemporaryMigration,

        /// <summary>
        /// This code is tightly coupled with other code, making it difficult to change or extend independently.
        /// Use this flag to indicate that refactoring may be needed to improve modularity and maintainability,
        /// and to indicate the relative 'untangling' effort that would be required by anyone attempting changes.
        /// </summary>
        TightlyCoupled,

        /// <summary>
        /// Marks code that must be thread-safe due to concurrent usage in multi-threaded or asynchronous environments.
        /// Use this flag to identify areas where careful synchronization or thread-safety mechanisms are required.
        /// This is an extra visibility to developers to be careful with changing this section of code.
        /// </summary>
        ThreadSafetyRequired,

        /// <summary>
        /// Unclassified flag - 
        /// Default, but can be used to indicate that an assessment is pending, and potentially required (or desired).
        /// </summary>
        Unknown,

        /// <summary>
        /// Code is currently being worked on and is not yet complete (i.e. currently in flux).
        /// Use this flag to indicate that the code is in an intermediate state and may not function as expected.
        /// Other developers should be aware that this code is subject to change and should coordinate with the author before making modifications.
        /// </summary>
        [Author("Warren James", 2025)]
        WorkInProgress,
    }
}
