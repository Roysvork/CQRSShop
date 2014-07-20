CQRSShop
========

This is a fork of the Simple CQRS and eventsourcing with eventstore and elasticsearch [by mastoj](https://github.com/mastoj/CQRSShop)

### Philosophy

The functionality of this project is identical to the parent and all tests pass, but it reflects my personal opinions on best practices for implementing DDDesigns in C#

Most of the changes center around the belief that project structure should reflect the conceptual layers of your application, and most significantly that the Domain project should not entertain any concerns that are not to do with business decision making, for example command handlers.

The introduction of a project representing the Application Layer is the key difference. All command handlers reside here, and **this project is the ONLY one in the solution allowed to reference the domain**. From this perspective - even though methods on aggregate roots may be public - integrity of the domain is assured.

This extra granularity **allows for Entities to be scoped more restrictively than Aggregate Roots**... the former can now be made inaccessible to command handlers which was not possible in the parent sample. It is for this reason that the project structure used in this sample is important and should be preferred over mixing handlers and aggregate roots in the same project.


### Use of static class facade to Raise Events

This is perhaps the most contraversial aspect of this implementation. It is important to note that the static class is **simply a facade** for an Action&lt;object&gt; that allows us to avoid taking a dependency in our domain. While this is a grand debate that I will not enter into here, I will at least summarise the thinking behind choosing this approach.

If we approach the problem theoretically and from outside the current constraints of the C# language... the ideal scenario would be if we had the ability to raise events natively in code, i.e via a first class language construct such as what we use for exceptions. This would allow us to implement our decision making code without polluting it by tracking code or having to return events from aggregate roots.

Given that this is not possible at present... it was decided that use of a static class facade over an event dispatcher scoped to logical call context so as to be thread safe was the next best thing. I consider this to be the least worst option as it allows us to keep our Aggregate roots clean and free of non-decision making concerns.


### Key Differences from the parent sample

* F# Contracts have been converted to C# in the CQRSShop.Types project and have been joined by some exceptions.
* Domain project now only depends on Types and nothing else
* Infrastructure project now only deals with technical/persistence concerns
* New project CQRS.Application to represent the Application Layer.
* Aggregate roots are now public (but referenced ONLY by application project), all entities are internal.
* Domain project is no longer responsible for wireup of apply functions on aggregate roots to matching events (referred to as transitions in the parent sample) - This is done in CQRSShop.Application
* DomainEntry class has been replaced with Shop.cs and resides in CQRS.Application
* Command Handlers no longer reside in the same project as the domain, they can be found in CQRSShop.Application
* Events and Commands no longer implement interfaces - knowlege of Id properties is now wired up in the CQRSShop.Application project
* Aggregate roots no longer track their own uncommitted events and no longer require base classes
* Some methods in aggregate roots and command handlers have been simplified and no longer return anything
* Event storage concerns have been seperated from the domain repository implementation - now in IEventBus
* DomainRepository implementation has been simplified, renamed to Repository and moved to CQRS.Application

### Caveats

* Web application may or may not be working
* Services are unavailable
* Only in-memory implementations of command and event bus at present
