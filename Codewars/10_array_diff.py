#task is to make a function, which subtracts one list from another and returns the result.

def array_diff(a, b):
    result = []
    for x in a:
        found = False
        for y in b:
            if x == y:
                found = True
                break
        if not found:
            result.append(x)
    return result