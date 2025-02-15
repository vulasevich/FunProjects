 #task is to make a function which formats a duration, given as a number of seconds, in a human-friendly way.
def format_duration(seconds):
    if seconds == 0:
        return "now"
    
    years = seconds // 31536000
    seconds %= 31536000
    
    days = seconds // 86400
    seconds %= 86400
    
    hours = seconds // 3600
    seconds %= 3600
    
    minutes = seconds // 60
    seconds %= 60
    
    result = ""
    
    if years:
        result += str(years)
        if years == 1:
            result += " year"
        else:
            result += " years"
    
    if days:
        if result:
            result += ", "
        result += str(days)
        if days == 1:
            result += " day"
        else:
            result += " days"
    
    if hours:
        if result:
            result += ", "
        result += str(hours)
        if hours == 1:
            result += " hour"
        else:
            result += " hours"
    
    if minutes:
        if result:
            result += ", "
        result += str(minutes)
        if minutes == 1:
            result += " minute"
        else:
            result += " minutes"
    
    if seconds:
        if result:
            result += ", "
        result += str(seconds)
        if seconds == 1:
            result += " second"
        else:
            result += " seconds"
    
    last_comma = result.rfind(", ")
    if last_comma != -1:
        result = result[:last_comma] + " and" + result[last_comma + 1:]
    
    return result
