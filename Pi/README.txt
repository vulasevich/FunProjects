The test project calculates pi using a grid method.
Instead of randomly selecting the coordinates of a point, the program chooses points at regular intervals. This removes randomness from the results.
The regular Monte Carlo method uses the random function many times, which takes up computing power and causes delay

The grid algorithm ran for about a minute, checking 100,000 squared points and returned 3.1416325452, which matches the value of pi to approximately 99.9987%
The Monte Carlo algorithm did only 60 000 squared points in one minute and returned 3,1416047232 , which matches the value of pi to approximately 99.9993% 
As a result, the grid method calculates points faster than the Monte Carlo algorithm, but with less accuracy. However, there is room for improvement.
