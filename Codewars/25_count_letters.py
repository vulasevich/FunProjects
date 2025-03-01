#task is to make a function that counts how many times each letter appears in the string

s = "aaaabbaabbbbcbbbbbcccccc"

counts = []
characters = []

for i in range(len(s)):
    char = s[i]
    found = False
    
    for j in range(len(characters)):
        if characters[j] == char:
            counts[j] += 1
            found = True
            break
    
    if not found:
        characters.append(char)
        counts.append(1)

compressed = ""
for i in range(len(characters)):
    compressed += str(counts[i]) + characters[i] + " "

print(compressed.strip())
