# Redox Code Test

**Make a fork of this repository and complete the following exercises in c#. Once completed, submit a pull request with your solutions.**

## Exercise 1: LINQ Query

1. Create a list of integers from 1 to 100.
2. Use LINQ to find all even numbers in the list and print them.
3. Use a loop to find all of the numbers in the original list that are divisible by 3 or 5, but not 3 and 5.
   The result should be:

> 3,  5,  6,  9, 10, 12, 18, 20, 21, 24, 25, 27, 33, 35, 36, 39, 40, 42, 48, 50, 51, 54, 55, 57, 63, 65, 66, 69, 70, 72, 78, 80, 81, 84, 85, 87, 93, 95, 96, 99

## Exercise 2: Event Scheduler

1. Create a class named 'Event' with properties 'Name', 'Location', 'DateTime'.
2. Create a class `EventScheduler` with a list of `Event`. Add methods to `ScheduleEvent`, `CancelEvent`, and `GetUpcomingEvents`.
3. Implement a feature to prevent double-booking, where two events are scheduled for the exact same time.
4. Save the Events to persistent storage. Any persistant storage can be used.

## Implementation Features

- Upgraded the project to .NET 6.0 (enable running on MAC OS)
- Added CLI interface for all operations
- Implemented EventScheduler with O(1) conflict detection using HashSet
- Added JSON persistence (persistantEvents.json)
- Added test suite for Exercise 2

## Quickstart Guide

- Start program with:  `dotnet run` to get a list of possible CLI commands
- Run Exercise1 with `dotnet run -- testexercise1`
- Run Exercise2 tests with `dotnet run -- testexercise2`
- Schedule an exampe event with `dotnet run -- schedule "Breakfast" "Kitchen" "2026-01-01"`
- Cancel Event with `dotnet run -- cancel "Breakfast"`
- Get a list of upcoming events with `dotnet run -- getupcomingevents`