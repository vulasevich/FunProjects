#task is to make a function that converts dash/underscore delimited words into camel casing.
def to_camel_case(text):
    if not text:
        return text
    
    words = []
    word = ''
    
    for char in text:
        if char == '-' or char == '_':
            if word:
                words.append(word)
                word = ''
        else:
            word += char
    
    if word:
        words.append(word)

    for i in range(1, len(words)):
        words[i] = words[i].capitalize()

    return words[0] + ''.join(words[1:])
