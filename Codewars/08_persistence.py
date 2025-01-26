#task is to make a function that takes in a positive parameter num and returns its multiplicative persistence
#which is the number of times you must multiply the digits in num until you reach a single digit.
def persistence(num):
    count = 0
    while num >= 10:
        product = 1
        while num > 0:
            product *= num % 10
            num = num // 10
        num = product
        count += 1
    return count
