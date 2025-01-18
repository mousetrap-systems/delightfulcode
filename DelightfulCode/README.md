DOCUMENTATION TBD

Contains: Structures to help your code read like a story.
It's intended to help promote conversation and discussion,
especially where the age of the code is spread out across many teams and many timeframes.

## Features

1. HealthAttribute - decorate your code with health markers, to assist future cleanups

2. AuthorAttribute - allows you to denote who wrote what, and when

3. String extensions: HasSomeValue(), IsBlank() and others

4. CircularBuffer - I could never find an implementation I liked so I made my own > properly tested.

# Version 1.0.6 - Jan 18, 2025

- Added 'CircularBuffer' class, which is the official stable version for many of my other projects
- Added 'Randomness' utility class which has some methods for creating identifiers
- Updated 'CodeStability' enumeration with some more additions, things I've encountered over 2024.
- All files now have a blank line at the end, yes I'm getting with the times

# Version 1.0.1 - December 02, 2024

- Tested the mechanism for upgrading version of nuget. No actual changes

# Version 1.0.0 - December 01, 2024

- First official release non-alpha, and with blessing to use namespace from Rob Selbach


NOTES:
- GPT-4 is occasionally used to refine the xml comments.
- We have official permission to use this namespace, from Roberto Selbach (November 2024)
- https://isitdoneyet.org/ is still coming soon