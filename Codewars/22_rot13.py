#task is to make a function that takes a string and returns the string ciphered with Rot13
#ROT13 is a simple letter substitution cipher that replaces a letter with the letter 13 letters after it in the alphabet

def rot13(input_string):
    result = ''
    
    for char in input_string:
        if 'A' <= char <= 'Z':
            base = ord('A')
            position = ord(char) - base
            new_position = (position + 13) % 26
            new_char = chr(base + new_position)
            result += new_char

        elif 'a' <= char <= 'z':
            base = ord('a')
            position = ord(char) - base
            new_position = (position + 13) % 26
            new_char = chr(base + new_position)
            result += new_char

        else:
            result += char
    
    return result