 #task is to make an algorithm to check if a given string, s, can be formed from two other strings, part1 and part2.
def is_merge(s, part1, part2):
    i, j = 0, 0
    for char in s:
        if i < len(part1) and part1[i] == char:
            i += 1
        elif j < len(part2) and part2[j] == char:
            j += 1
        else:
            return False
    return i == len(part1) and j == len(part2)
