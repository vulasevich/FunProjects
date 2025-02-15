#task is to write a funtion luck_check(str), which returns true/True if argument is string decimal representation of a lucky ticket number, or false/False for all other numbers. 
#the number is lucky if sum of digits on the left half of its number was equal to the sum of digits on the right half
#if the length is odd, function should ignore the middle number when adding the halves.
def luck_check(ticket):
    n = len(ticket)
    
    mid = n // 2
    
    if n % 2 == 0:
        left = ticket[:mid]
        right = ticket[mid:]
    else:
        left = ticket[:mid]
        right = ticket[mid+1:]
    
    left_sum = 0
    for char in left:
        left_sum += int(char)
    
    right_sum = 0
    for char in right:
        right_sum += int(char)
    
    if left_sum == right_sum:
        return True
    else:
        return False
