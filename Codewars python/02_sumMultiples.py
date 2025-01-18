#task is to make a function that returns the sum of all the multiples of 3 or 5 below the number passed in.
#Additionally, if the number is negative, return 0

def solution(number):
    total_sum = 0

    if number <= 0:
        return 0
    
    for i in range(number):
        if i % 3 == 0 ^ i % 5 == 0:
            total_sum += i
    
    return total_sum
