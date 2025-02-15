#task is to make a function that takes a string input, and returns the first character that is not repeated anywhere in the string.
def first_non_repeating_letter(s: str) -> str:
    char_count = {}
    
    for char in s:
        char_lower = char.lower()
        char_count[char_lower] = char_count.get(char_lower, 0) + 1
    
    for char in s:
        if char_count[char.lower()] == 1:
            return char
    
    return ""