#task is to make a function that can take any non-negative integer as an argument and return it with its digits in descending order. 
#Essentially, rearrange the digits to create the highest possible number.
#I used a sorting method called 'insertion sort' to rearrange the digits in descending order

def descendingOrder(n):
    digits = list(str(n))
    sorted_digits = []

    for digit in digits:
        inserted = False
        for i in range(len(sorted_digits)):
            if digit > sorted_digits[i]:
                sorted_digits.insert(i, digit)
                inserted = True
                break
        if not inserted:
            sorted_digits.append(digit)

    return int(''.join(sorted_digits))  
    