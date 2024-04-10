import geocoder
import requests

#Pido ubicacion al usuario

def obtener_ubicacion():
    try:
        # Utiliza el proveedor 'ipinfo' para obtener la ubicación basada en la dirección IP del usuario
        g = geocoder.ipinfo('me')
        
        if g.ok:
            ciudad = g.city
            ubicacion = f"{ciudad}"
            return ubicacion
        else:
            return "No se pudo obtener la ubicación del usuario"

    except Exception as e:
        return "No se pudo obtener la ubicación del usuario"

#Fin de pedida de ubicacion al usuario

#conseguir hora y dia en el que está presente el usuario 

#en desUso, ya que estos text area me lo ahorro usando un calendar, ademas me sirve para poder usar la api y no tener 
#hacer tanta cosa para poder usar la api.

def texto_a_variable_hd(a, b):
    hora = a.get("1.0", "end-1c")
    dia = b.get("1.0", "end-1c") 
    #Llamar a la api para que con estas variables las utilice para poder mostrar el clima

#fin de conseguir datos

#obtener clima de la ciudad que ingresa el usuario
la_key = 'd2d148314fd201cae0214bd241cc4583'

def obtener_clima_actual(ciudad, fecha):
    url = f"http://api.openweathermap.org/data/2.5/weather?q={ciudad}&appid={la_key}&units=metric"
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

#Actualizo fecha



#finalizo actualizar fecha