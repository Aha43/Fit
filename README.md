# Fluent Integration Test (FIT)

# WORK IN PROGRESS

FIT is a .NET integration test framework where a *case* (i.e. usecases) is defined by a sequence of *act*s that acts on the system being tested.
Unlike unit test *act*s in a case is not independent: An *act* start acting on the system in a state caused by the *act*s that acted before it in the *case*. *Act*s are defined by the interface `IActor`.
The implementation of the interface can be used to perform many acts both in a *case* and across *cases*.
Implementations of the `IACtor` interface not only act on the system being testet but make claims of the system state after the act.



# WORK IN PROGRESS