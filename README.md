# Reflection Performance Test Application

Simple test application that I created, to test hw fast/slow Reflection is compared to other options.

This has been created, because "reflection is slow" - but it depends on the usecase, and nobody could tell how slow "slow" is meant to be.
This is not really about reflection, it's about making implementation decisssions, and checking if assumptions are true.


This is totally unscientific, just to get an overview on speed. 
* we do not use an accurate timer/stopwatch
* we do not do a warmup
* some tests do not make sence, since e.g. in reflection, we already know the instance and type,.... so we do the same thing 3 times (via reflection).
* ...

Testcase:
1. create a new instance of "DummyClass"
2. set a property (Name)
3. call a Method (GetHello())

Test execution
* we have 10 executions per implementation
* per execution we call Init() to check how long it takes to get the required cached data, depending on implementation.
* per iteration we: 
1. create a new instance
2. set a property
3. call a Method (GetHello())


Test implementations:
* Manual (Hardcoded): manual written code, which a developer would normally write to perform the actions. This should be the fastest
* Delegates: Uses delegates (after using reflection to get the method;property,..) to perform the tests. This should emit code underneath, so it should be similar to hand written code. IMPORTANT: i made an ugly implementation, so it's not all delegates.
* Compiled expressions: use expressions to perfor mthe operations. should be similar to delegates.
* Dynamic: use the dynamic keyowrd for 2 of the 3 operations. I ugess this is slower that the approaches above, but it should use a lot of DLR features underneath for a good enough performance.
* Reflection: use reflection for every operation, never caching any information. (this should be the slowest)
* Reflection with caching: metadata (reflection information, like propertyinfo,.. are cached.) - this should be slower than the operations that emit IL code, but much faster than the uncached reflection test.

I also wanted to do a Test that manually emits IL code (because it's fun) but then I noticed I have better things to do with my spare time, and the delegates/expression approach should be similar :)



Again, you usecase might be totally different, but at least it might gie ou an overview/idea. It might be ok to do reflection if it is an operation that is only executed once a minute (so your crappy business app with all of the database access might be already slow - you might want to improve that first --> this is what I do most of the time :)), but it might be a bad idea to do reflection in a tight game loop.

Here is the output from my old Intel I-5 laptop, by using "dotnet run":

Welcome - this is a test application to  test performace of Reflection and other options.
Starting test for: Hardcoded, manual - nr of iterations per repetition: 1000000 - repetitions: 10
Fastest for Hardcoded, manual:00:00:00.2210118
Slowest for Hardcoded, manual:00:00:00.3730051
Average for Hardcoded, manual:00:00:00.2924485
**********************************************
Starting test for: Compiled expressions - nr of iterations per repetition: 1000000 - repetitions: 10
Fastest for Compiled expressions:00:00:00.2268218
Slowest for Compiled expressions:00:00:00.3760091
Average for Compiled expressions:00:00:00.3162148
**********************************************
Starting test for: Delegates - nr of iterations per repetition: 1000000 - repetitions: 10
Fastest for Delegates:00:00:00.2540550
Slowest for Delegates:00:00:00.3722743
Average for Delegates:00:00:00.3064953
**********************************************
Starting test for: Dynamic - nr of iterations per repetition: 1000000 - repetitions: 10
Fastest for Dynamic:00:00:00.2866816
Slowest for Dynamic:00:00:00.4654835
Average for Dynamic:00:00:00.3456718
**********************************************
Starting test for: Reflection - nr of iterations per repetition: 1000000 - repetitions: 10
Fastest for Reflection:00:00:07.8654009
Slowest for Reflection:00:00:10.2571343
Average for Reflection:00:00:08.2664987
**********************************************
Starting test for: Reflection with caching - nr of iterations per repetition: 1000000 - repetitions: 10
Fastest for Reflection with caching:00:00:00.6687227
Slowest for Reflection with caching:00:00:00.7462454
Average for Reflection with caching:00:00:00.7085132
**********************************************
