#task is to make a function, which takes a non-negative integer (seconds) as input and returns the time in a human-readable format (HH:MM:SS)

def make_readable(seconds: int) -> str:

    hours = seconds // 3600
    remaining_seconds = seconds % 3600
    minutes = remaining_seconds // 60
    secs = remaining_seconds % 60
    
    return f"{hours:02}:{minutes:02}:{secs:02}"