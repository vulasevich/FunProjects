#Task is to make a function that turns string into hashtag
#It must start with a hashtag (#).
#All words must have their first letter capitalized.
#If the final result is longer than 140 chars it must return false.
#If the input or the result is an empty string it must return false.
def generate_hashtag(s):
    if not s or len(s.strip()) == 0:
        return False
    words = s.split()
    hashtag = '#'
    for word in words:
        hashtag += word.capitalize()
    return hashtag if len(hashtag) <= 140 else False
