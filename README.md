# Fluent Integration Test (FIT)

# WORK IN PROGRESS

FIT is a .NET integration test framework where a *case* (i.e. usecases) is defined by a sequence of *act*s that acts on the system being tested when a *case* is run.
Unlike unit test *act*s in a case are not independent: An *act* start acting on the system in a state caused by the *act*s that acted before it in the *case*. *Act*s are defined by the interface `IActor`.
The implementation of the interface can be used to perform many acts both in a *case* and in more than one *case*.
Implementations of the `IACtor` interface not only act on the system being testet but make claims about (writing to an instance of `SystemClaims`) the system state after the act.
After an *act* have acted the claims are checked to be true by implementations of the `IAssertor` interface.
Separating *asserting* from *acting* means also side effects can be caught not only that an *act* did work as expected.
Both `IActor` and `IAssertor` gets the part of the system they need to access through dependency injection.

## Examples

### Defining cases

# WORK IN PROGRESS
