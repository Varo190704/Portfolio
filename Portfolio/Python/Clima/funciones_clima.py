#Imports

import requests

#Fin de imports

#obtener clima de la ciudad que ingresa el usuario

la_key = 'd2d148314fd201cae0214bd241cc4583'

def obtener_clima_actual(ciudad, fecha):
    url = f"http://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid={la_key}&units=metric&lang=es"
    respuesta = requests.get(url)
    datos_clima = respuesta.json()

    if respuesta.status_code == 200:
        temperatura = datos_clima['main']['temp']
        descripcion = datos_clima['weather'][0]['description']
        mensaje = f"El clima en {ciudad} en el dia {fecha}" + '\n' + f"es {descripcion} con una temperatura"+'\n'+ f"de {temperatura}°C"
        return mensaje
    else:
        return "Error al obtener el pronóstico del clima"

#fin de obtener clima de la ciudad que ingresa el usuario