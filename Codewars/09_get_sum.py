#Given two integers a and b, which can be positive or negative, find the sum of all the integers between and including them and return it. 

def get_sum(a, b):
    total = 0
    start = min(a, b)
    end = max(a, b)
    for i in range(start, end + 1):
        total += i
    return total