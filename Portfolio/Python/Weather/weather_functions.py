#imports

import requests

#End of imports

#Get weather of the city entered by the user
api_key = 'd2d148314fd201cae0214bd241cc4583'

def get_current_weather(city, date):
    url = f"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={api_key}&units=metric&lang=en"
    response = requests.get(url)
    weather_data = response.json()

    if response.status_code == 200:
        temperature = weather_data['main']['temp']
        description = weather_data['weather'][0]['description']
        message = f"The weather in {city} on {date}" + '\n' + f"is {description} with a temperature"+'\n'+ f"of {temperature}°C"
        return message
    else:
        return "Error fetching weather forecast"

#End of Get weather of the city entered by the user
