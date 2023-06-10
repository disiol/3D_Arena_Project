using System;
using System.Reflection;
using Player.Movement;
using Tolls;
using UnityEngine;

namespace Tests.TestsPlayMode
{
    public class GetAccessToPrivate
    {
        public object GetPrivateFieldValue(Type type, object instance, string fieldName)
        {
            // Get the private field "myPrivateField"
            FieldInfo field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            // Access the private field value
            object privateFieldValue = field.GetValue(instance);

            return privateFieldValue;
        }


        public void SetPrivateFieldValue(Type type, object instance, string name, Transform value)
        {
            // Get the private field by name
            FieldInfo fieldInfo = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the value of the private field
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, value);
            }
        }

        public void SetPrivateFieldValue(Type type, object instance, string name, Transform[] value)
        {
            // Get the private field by name
            FieldInfo fieldInfo = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the value of the private field
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, value);
            }
        }

        public void SetPrivateFieldValue(Type type, object instance, string name, GameObject value)
        {
            FieldInfo fieldInfo = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the value of the private field
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, value);
            }
        }

        public void SetPrivateFieldValue(Type type, object instance, string name, float value)
        {
            FieldInfo fieldInfo = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the value of the private field
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, value);
            }
        }

        public void SetPrivateFieldValue(Type type, object instance, string name, GameObject[] value)
        {
            FieldInfo fieldInfo = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the value of the private field
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, value);
            }
        }

        public void SetPrivateFieldValue(Type type, object instance, string name, BoxCollider value)
        {
            FieldInfo fieldInfo = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the value of the private field
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, value);
            }
        }
        
        public void SetPrivateFieldValue(Type type,  object instance, string name, Collider value)
        {
            FieldInfo fieldInfo = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            // Set the value of the private field
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(instance, value);
            }
        }

        public void GetPrivateMethod(Type type, string name)
        {
            // Get the private method "MyPrivateMethod"
            type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public object GetPrivateMethod(Type type, object instance, string name,object[] parameters)
        {
            // Get the private method "MyPrivateMethod"
            type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);

            MethodInfo method = type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance);
         
            object invoke = method.Invoke(instance, parameters);

            return invoke;
        }


       
    }
}