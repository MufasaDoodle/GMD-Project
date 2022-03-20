using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return typeof(Item).IsAssignableFrom(objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        
        Debug.Log(objectType.Name);
        if (objectType == typeof(Equipment))
        {
            return serializer.Deserialize(reader, typeof(Equipment));
        }

        if (objectType == typeof(Trash))
        {
            return serializer.Deserialize(reader, typeof(Trash));
        }

        throw new Exception("Could not determine item object type");
        
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        serializer.Serialize(writer, value);
    }
}
