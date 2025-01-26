#task is to make a function maskify, which changes all but the last four characters into '#'.
def maskify(string):
    if len(string) <= 4:
        return string
    
    return '#' * (len(string) - 4) + string[-4:]
