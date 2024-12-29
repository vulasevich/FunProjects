The test project calculates pi using a grid method.
Instead of randomly selecting the coordinates of a point, the program chooses points at regular intervals. This removes randomness from the results.

I compared two algorithms and found that the grid algorithm calculates pi faster and more accurately than the Monte Carlo method.
The regular Monte Carlo method uses the random function many times, which takes up computing power and causes delay
The grid algorithm ran for about a minute, checking 100,000 squared points and returned 3.1416325452, which matches the value of pi to approximately 99.9987%
