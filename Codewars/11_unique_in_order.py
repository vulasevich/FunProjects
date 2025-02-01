#task is to make a function function unique_in_order which takes as argument a sequence and returns a list of items 
#without any elements with the same value next to each other and preserving the original order of elements.


def unique_in_order(sequence):
    result = []
    
    if len(sequence) == 0:
        return result  
    
    previous = sequence[0]
    result.append(previous)
    
    index = 1
    while index < len(sequence):
        current = sequence[index]
        
        if current != previous:
            result.append(current)
        
        previous = current
        index += 1
    
    return result