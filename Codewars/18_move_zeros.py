#task is to make an algorithm that takes an array and moves all of the zeros to the end, preserving the order of the other elements.

def move_zeros(arr):
    non_zeros = [num for num in arr if num != 0]
    return non_zeros + [0] * (len(arr) - len(non_zeros))
