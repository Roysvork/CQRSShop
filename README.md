CQRSShop
========

This is a fork of the Simple CQRS and eventsourcing with eventstore and elasticsearch [by mastoj](https://github.com/mastoj/CQRSShop)

### Philosophy

The functionality of this project is identical to the parent and all tests pass, but it reflects my personal opinions on implementing DDDesigns in C#

Most of the changes center around the belief that project structure should reflect the conceptual layers of your application, and most significantly that the Domain project should not entertain any concerns that are not to do with business decision making, for example command handlers.

The introduction of a project representing the Application Layer is the key difference. All command handlers reside here, and **this project is the ONLY one in the solution allowed to reference the domain**. From this perspective - even though methods on aggregate roots may be public - integrity of the domain is assured.

This extra granularity **allows for Entities to be scoped more restrictively than Aggregate Roots**... the former can now be made inaccessible to command handlers which was not possible in the parent sample. It is for this reason that the project structure used in this sample is important and should be preferred over mixing handlers and aggregate roots in the same project.


### Key Differences

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
