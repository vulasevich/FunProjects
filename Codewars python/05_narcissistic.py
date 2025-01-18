def is_narcissistic(number):
    digits = str(number)
    num_digits = len(digits)
    
    if number == sum(int(digit) ** num_digits for digit in digits):
        return True
    return False
