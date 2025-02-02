#task is to make a function that takes an integer argument and returns a logical value true or false depending on if the integer is a prime.

def is_prime(n):
    if n <= 1:
        return False
    for i in range(2, int(n**0.5) + 1):
        if n % i == 0:
            return False
    return True